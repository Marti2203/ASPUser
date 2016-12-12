using UserApp.CommonFiles.DTO;
using UserApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Infrastructure
{
    public class CVService
    {
        public void Insert(CVDTO dto)
        {
            ATPEntities context = new ATPEntities();
            context.CV.Add(Convert(dto));
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            ATPEntities context = new ATPEntities();
            context.CV.Remove(context.CV.FirstOrDefault(element => element.ID == id));
            context.SaveChanges();
        }

        public CVDTO Get(int id) => Convert(new ATPEntities().CV.FirstOrDefault(element => element.ID == id));


        public void Edit(CVDTO dto)
        {
            ATPEntities context = new ATPEntities();
            CV entity = context.CV.FirstOrDefault(element => element.ID == dto.ID);
            entity.Address = dto.Address;
            entity.Education = dto.Education;
            entity.Email = dto.Email;
            entity.Experience = dto.Experience;
            entity.FirstName = dto.FirstName;
            entity.ID = dto.ID;
            entity.LastName = dto.LastName;
            entity.Qualities = dto.Qualities;
            entity.Picture = dto.PictureBytes;
            entity.PictureName = dto.PictureName;
            context.SaveChanges();
        }

        public bool Has(int id) => new ATPEntities().CV.FirstOrDefault(element => element.ID == id) != null;

        public IEnumerable<CVDTO> GetAll()
        {
            foreach (CV cv in new ATPEntities().CV)
                yield return Convert(cv);
        }

        private CVDTO Convert(CV entity) =>
            new CVDTO
            {
                Address = entity.Address,
                Education = entity.Education,
                Email = entity.Email,
                Experience = entity.Experience,
                FirstName = entity.FirstName,
                ID = entity.ID,
                LastName = entity.LastName,
                Qualities = entity.Qualities,
                PictureBytes = entity.Picture,
                PictureName = entity.PictureName
            };

        private CV Convert(CVDTO dto) =>
            new CV
            {
                Address = dto.Address,
                Education = dto.Education,
                Email = dto.Email,
                Experience = dto.Experience,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Qualities = dto.Qualities,
                Picture = dto.PictureBytes,
                PictureName = dto.PictureName
            };
    }
}
