namespace MyStats_Rest.Models.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        protected BaseModel()
        {
            //Id = Guid.NewGuid().ToString();
        }
    }
}
