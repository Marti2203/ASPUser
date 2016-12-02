using UserApp.CommonFiles.DTO;
using UserApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Infrastructure
{    
    //Service for communication between the Database and the Controller
    public class BoxServiceHack
    {
        private int starterID = new Random().Next();

        public void Insert(BoxDTOHack box)
        {
            ATPEntities _dbContext = new ATPEntities();//Create connection
            start:
            try
            {
                _dbContext.User.Add(Convert(box));//Add box to database
            }
            catch (System.Data.DataException)
            {
                starterID = new Random().Next();
                goto start;
            }
            _dbContext.SaveChanges();//ALWAYS SAVE CHANGES
        }//Add a box to the database

        public void Delete(string id)
        {
            ATPEntities _dbContext = new ATPEntities();//Create connection
            //Remove box if he exists
            _dbContext.User.Remove(_dbContext.User.Where(entity => entity.EMAIL == id).FirstOrDefault());
            _dbContext.SaveChanges();
        }//Remove a box with the specified ID
   
        public void Edit(BoxDTOHack dto)
        {
            ATPEntities _dbContext = new ATPEntities();
            User old = _dbContext.User.Where(entity => entity.EMAIL == dto.ID).FirstOrDefault();
            User newUser = Convert(dto);
            foreach (PropertyInfo property in old.GetType().GetProperties().Where(property => property.Name != "ID"))
                property.SetValue(old, property.GetValue(newUser));
            _dbContext.SaveChanges();
        }//Edit a box with the specified by the DTO ID

        public BoxDTOHack Get(string id)
        {
            ATPEntities _dbContext = new ATPEntities();//Create connection
            User user = _dbContext.User.Where(entity => entity.EMAIL == id).FirstOrDefault();//Find box with id or return null
            return Convert(user);
        }//Get a box with the specified ID

        public IEnumerable<BoxDTOHack> GetAll()
        {
            ATPEntities _dbContext = new ATPEntities();//Create connection
            foreach (User user in _dbContext.User)
            {
                yield return Convert(user);
            }
        }//Get all boxes

        #region Mappers
        /*
         * Name : Colour
         * Password: Material
         * About: Weight,Length,Height,Width
         * Email: GUID
         */
        private User Convert(BoxDTOHack box)
        {
            User boxObject = new User
            {
                USERNAME = box.Colour,
                PASSWORD = box.Material,
                EMAIL = box.ID,
            };
            #region Add Dimensions To User Entity
            StringBuilder builder = new StringBuilder();

            builder.Append(box.Weight);
            builder.Append(',');
            builder.Append(box.Length);
            builder.Append(',');
            builder.Append(box.Height);
            builder.Append(',');
            builder.Append(box.Width);

            boxObject.ABOUT = builder.ToString();
            #endregion

            boxObject.ID = starterID++;
            boxObject.GENDER = "b";
            boxObject.SECRET_A = "A BOX";
            boxObject.SECRET_Q = "What are you?";

            return boxObject;
        }

        private BoxDTOHack Convert(User user)
        {
            BoxDTOHack box = new BoxDTOHack()
            {
                Colour = user.USERNAME,
                Material = user.PASSWORD,
                ID = user.EMAIL
            };

            string[] elements = user.ABOUT.Split(',');

            #region Get Dimensions From User Entity
            box.Weight = int.Parse(elements[0]);
            box.Length = int.Parse(elements[1]);
            box.Height = int.Parse(elements[2]);
            box.Width = int.Parse(elements[3]);
            #endregion

            return box;
        }
#endregion

    }
}
