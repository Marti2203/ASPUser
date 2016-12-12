using UserApp.DataAccess;
using UserApp.CommonFiles;
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
    public class UserService
    {

        //Insert a user to the database
        public void Insert(UserDTO user)
        {
            ATPEntities _dbContext = new ATPEntities(); //Database context(database connection)
            _dbContext.USER_NEW_NEW.Add(Convert(user));            //Map DTO to Database Object
            _dbContext.SaveChanges();//ALWAYS SAVE AFTER BEING DONE. Make only one transfer to the database for safety reasons
        }//Adds a user to the database

        public bool Has(int id)
        {
            ATPEntities _dbContext = new ATPEntities(); //Database context(database connection)
            //Return true if there exists a user
            return _dbContext
            .USER_NEW_NEW
            .FirstOrDefault(entity => entity.ID==id) != null;
        }//Get a user by a specified ID

        public void Delete(int id)
        {
            ATPEntities _dbContext = new ATPEntities(); //Database context(database connection)
            //Remove user if there exists one with the specified username
            _dbContext.USER_NEW_NEW.Remove(_dbContext.USER_NEW_NEW.FirstOrDefault(user => user.ID==id));
            _dbContext.SaveChanges();//ALWAYS SAVE AFTER BEING DONE. Make only one transfer to the database for safety reasons
        }//Delete a user by a specified ID

        public void Edit(UserDTO dto)
        {
            ATPEntities _dbContext = new ATPEntities(); //Database context(database connection)
            USER_NEW_NEW userOld = _dbContext.USER_NEW_NEW.FirstOrDefault(user => user.USERNAME == dto.Username);
            USER_NEW_NEW userNew = Convert(dto);
            //Map values from new entity to the existing version because this is how it must be done
            foreach (PropertyInfo property in userOld.GetType().GetProperties())
            {
                property.SetValue(userOld, property.GetValue(userNew));
            }
            _dbContext.SaveChanges();//ALWAYS SAVE AFTER BEING DONE. Make only one transfer to the database for safety reasons
        }//Edit a user by a specified from the DTO ID

        public UserDTO Get(int id)
        {
            ATPEntities _dbContext = new ATPEntities(); //Database context(database connection)
            //Get User if there exists one with the specified username and convert to DTO
            USER_NEW_NEW userEntity = _dbContext.USER_NEW_NEW.FirstOrDefault(user => user.ID == id);
            return Convert(userEntity);
        }//Get a user by a specified ID

        public IEnumerable<UserDTO> GetAll()
        {
            ATPEntities _dbContext = new ATPEntities(); //Database context(database connection)
            foreach (USER_NEW_NEW user in _dbContext.USER_NEW_NEW)
            {
                yield return Convert(user);
            }
        }//Get all users

        #region Mappers

        private UserDTO Convert(USER_NEW_NEW userEntity) => new UserDTO
        {
            Username = userEntity.USERNAME,
            Password = userEntity.PASSWORD,
            About = userEntity.ABOUT,
            Email = userEntity.EMAIL,
            Gender = userEntity.GENDER,
            SecretAnswer = userEntity.SECRET_A,
            SecretQuestion = userEntity.SECRET_Q,
            ID=userEntity.ID
        };
        private USER_NEW_NEW Convert(UserDTO dto) => new USER_NEW_NEW
        {
            USERNAME = dto.Username,
            ABOUT = dto.About,
            EMAIL = dto.Email,
            GENDER = dto.Gender,
            PASSWORD = dto.Password,
            SECRET_A = dto.SecretAnswer,
            SECRET_Q = dto.SecretQuestion,
            ID = dto.ID
        };
        #endregion
    }
}
