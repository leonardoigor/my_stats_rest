using Confluent.Kafka;
using MyStats_Rest.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MayStats_Infra.Repositories
{
    public class KafkaRepository
    {
        private ElasticRespository _elasticRespository;

        public KafkaRepository()
        {
            _elasticRespository = new ElasticRespository("http://localhost:9200");

        }
        public async Task SendAsync<TQueueType>(object Data) where TQueueType : struct
        {
            // Get the name of TQueueType
            string typeName = typeof(TQueueType).Name;

            // Convert the type name to a hyphenated string
            string KafkaTopic = ConvertToHyphenated(typeName);

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                Acks = Acks.All,  // Ensure all replicas acknowledge the message
                MessageTimeoutMs = 5000
            };
            using (var producer = new ProducerBuilder<string, string>(producerConfig).Build())
            {
                var message = JsonConvert.SerializeObject(Data);
                try
                {
                    var dr = await producer.ProduceAsync(KafkaTopic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = message });
                    //Console.WriteLine($"Mensagem '{dr.Value}' enviada para o tópico '{dr.Topic}' com offset {dr.Offset}.");
                }
                catch (ProduceException<string, string> e)
                {
                    Console.WriteLine($"Kafka produce error: {e.Error.Reason}");
                }
            }

        }
        public void ConsumeAsync<TQueueType>(CancellationToken cancellationToken) where TQueueType : struct
        {

            ConsumeExec<TQueueType>(cancellationToken);

        }

        private void ConsumeExec<TQueueType>(CancellationToken cancellationToken) where TQueueType : struct
        {
            try
            {
                string typeName = typeof(TQueueType).Name;

                // Convert the type name to a hyphenated string to match the producer topic
                string KafkaTopic = ConvertToHyphenated(typeName);

                var consumerConfig = new ConsumerConfig
                {
                    GroupId = "kafka-group-1", // Unique group ID for your consumer group
                    BootstrapServers = "localhost:9092",
                    AutoOffsetReset = AutoOffsetReset.Earliest, // Start consuming from the earliest message if no offset is found
                    EnableAutoCommit = true,  // Automatically commit the offsets of consumed messages
                };

                using (var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build())
                {
                    consumer.Subscribe(KafkaTopic);
                    try
                    {
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            var consumeResult = consumer.Consume(cancellationToken);
                            Console.WriteLine($"Mensagem recebida do tópico '{consumeResult.Topic}': {consumeResult.Message.Value}");
                            var model = JsonConvert.DeserializeObject<Stats>(consumeResult.Message.Value);

                            try

                            {
                                Console.WriteLine($"Sending to ES {KafkaTopic}");
                                var el_request = _elasticRespository.CreateAsync(model);
                                el_request.Wait();
                            }
                            catch (Exception e)
                            {

                                Console.WriteLine($"Error on ES {e}");
                            }
                            finally
                            {
                                Console.WriteLine($"Sended to ES {KafkaTopic}");

                            }

                        }
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Kafka consume error: {e.Error.Reason}");
                    }
                    finally
                    {
                        consumer.Close(); // Close the consumer when finished
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Kafka consume error: {e}");

                Thread.Sleep(3);
                ConsumeExec<TQueueType>(cancellationToken);
            }
        }

        private string ConvertToHyphenated(string input)
        {
            // Use regex to insert hyphens between words
            string result = Regex.Replace(input, "(?<!^)([A-Z])", "-$1").ToLower();
            return result;
        }
    }
}
