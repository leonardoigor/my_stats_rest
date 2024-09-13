using MayStats_Infra.Controller;
using MayStats_Infra.Interfaces.Repositories.Base;
using MayStats_Infra.Repositories;
using MyStats_Rest.Models;

namespace MyStats_Rest.Controllers
{

    public class StaticController : GenericController<string, Stats>
    {
        private KafkaRepository _kafkaRepository;

        public StaticController(IEFRepository<string, Stats> eFRepository) :base(eFRepository)
        {
            _kafkaRepository = new KafkaRepository();
        }


    }
}
