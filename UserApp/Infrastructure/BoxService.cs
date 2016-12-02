using UserApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.CommonFiles.DTO;
using System.Reflection;

namespace UserApp.Infrastructure
{
    //Service for communication between the Database and the Controller
    public class BoxService
    {
        public void Insert(BoxDTO box)
        {
            ATPEntities _dbContext = new ATPEntities();//Create connection
            _dbContext.BOX.Add(Convert(box));//Add box to database

            _dbContext.SaveChanges();//Save Changes to the database
        }//Adds a box to the database

        public void Delete(int id)
        {
            ATPEntities _dbContext = new ATPEntities();//Create connection
            //Remove box if it exists
            _dbContext.BOX.Remove(_dbContext.BOX.Where(entity => entity.ID == id).FirstOrDefault());
            _dbContext.SaveChanges();//Save Changes to the database
        }//Removes a box with the specified ID

        public void Edit(BoxDTO dto)
        {
            ATPEntities _dbContext = new ATPEntities();//Create connection
            BOX old = _dbContext.BOX.Where(entity => entity.ID == dto.ID).FirstOrDefault();//Get the old box
            BOX newBOX = Convert(dto);//Create a new box from a conversion
            foreach (PropertyInfo property in old.GetType().GetProperties().Where(property => property.Name != "ID"))
                property.SetValue(old, property.GetValue(newBOX));// set the values of the old box to the values of the new one
            _dbContext.SaveChanges();//Save Changes to the database
        }//Edit a box with the specified by the DTO ID

        public BoxDTO Get(int id)
        {
            ATPEntities _dbContext = new ATPEntities();//Create connection
            BOX box = _dbContext.BOX.Where(entity => entity.ID == id).FirstOrDefault();//Find box with id or return null
            return Convert(box);//Convert to DTO and return
        }//Gets a box with the specified ID

        public IEnumerable<BoxDTO> GetAll()
        {
            ATPEntities _dbContext = new ATPEntities();//Create connection
            foreach (BOX box in _dbContext.BOX)
            {
                yield return Convert(box);
            }
        }//Gets all boxes as an IEnumerable

        #region Mappers
        public BoxDTO Convert(BOX box)
        => new BoxDTO
        {
            Colour = box.COLOR,
            Height = box.HEIGHT,
            Length = box.LENGTH,
            Material = box.MATERIAL,
            Weight = box.WEIGHT,
            Width = box.WIDTH,
            ID = box.ID
        };

        public BOX Convert(BoxDTO dto)
        => new BOX
        {
            COLOR = dto.Colour,
            HEIGHT = dto.Height,
            LENGTH = dto.Length,
            MATERIAL = dto.Material,
            WEIGHT = dto.Width,
            WIDTH = dto.Weight,
            ID = dto.ID
        };
        #endregion
    }
}
