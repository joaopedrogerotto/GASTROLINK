using APIGastroLink.Models;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOMesa {
        public void Insert(string Numero);
        public void Update(Mesa Mesa);
        public void Delete(Mesa Mesa);
        public List<Mesa> SelectAll();
        public void UpdateLayout(List<Mesa> listMesa);
    }
}
