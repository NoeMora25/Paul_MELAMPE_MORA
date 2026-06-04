namespace Paul_MELAMPE_MORA.Classes
{
    public enum allergene_nom { Cereales_gluten, Oeufs, Lait, Fruit_coque, Soja, Sesame, Sulfites }

    public class Allergenes : ICrud<Allergenes>
    {
        private int allergene_id;
        private string allergene_nom;

        public Allergenes()
        {
        }

        public Allergenes(string allergene_nom)
        {
            this.Allergene_nom = allergene_nom;
        }

        public int Allergene_id
        {
            get
            {
                return this.allergene_id;
            }

        }

        public string Allergene_nom
        {
            get
            {
                return this.allergene_nom;
            }

            set
            {
                this.allergene_nom = value;
            }
        }

        public int Create()
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public List<Allergenes> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<Allergenes> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }
    }
}
