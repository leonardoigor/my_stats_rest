using MyStats_Rest.Models.Base;

namespace MyStats_Rest.Models
{
    public class Stats : BaseModel
    {
        public Stats():base()
        {
         
        }
        public float Weigth { get; set; }
        public float IMC { get; set; }
        public float FatPercentage { get; set; }
        public float FatWeight { get; set; }
        public float SkeletalMuscleMassPercentage { get; set; }
        public float WeightSkeletalMuscleMassPercentage { get; set; }
        public float MuscleMassRecordPercentage { get; set; }
        public float WaterPercentage { get; set; }
        public float WaterWeight { get; set; }
        public float VisceralFat { get; set; }
        public float Bones { get; set; }
        public float Metabolism { get; set; }
        public float ProteinPercentage { get; set; }
        public float ObesityPercentage { get; set; }
        public float MetabolicAge { get; set; }
        public float LBM { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }

    }
}
