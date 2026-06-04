using Npgsql;
using System.Data;

namespace Paul_MELAMPE_MORA.Classes
{

    public class Employe : ICrud<Employe>
    {
        private int employe_id;
        private string login;
        private string role;

        public Employe()
        {
        }

        public Employe(int employe_id, string login, string role)
        {
            this.Employe_id = employe_id;
            this.Login = login;
            this.Role = role;
        }

        public int Employe_id
        {
            get
            {
                return this.employe_id;
            }

            set
            {
                this.employe_id = value;
            }
        }

        public string Login
        {
            get
            {
                return this.login;
            }

            set
            {
                this.login = value;
            }
        }

        public string Role
        {
            get
            {
                return this.role;
            }

            set
            {
                this.role = value;
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

        public List<Employe> FindAll()
        {
            List<Employe> lesEmployes = new List<Employe>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from  employe ;"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesEmployes.Add(new Employe((int)dr["employe_id"], (String)dr["login"], (String)dr["role"]));
            }

            return lesEmployes;
        }

        public List<Employe> FindBySelection(string criteres)
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
