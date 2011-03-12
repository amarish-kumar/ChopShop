using System;
using System.Collections.Generic;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Models.ViewModel
{
    public class EditCategory
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public void FromEntity(Category categoryEntity)
        {
            Id = categoryEntity.Id;
            Name = categoryEntity.Name;
            Description = categoryEntity.Description;
        }
    }
}