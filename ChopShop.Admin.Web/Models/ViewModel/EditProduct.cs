using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ChopShop.Localisation;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Models.ViewModel
{
    public class EditProduct : IValidation
    {
        public int Id { get; set; }
        
        [LocalisedDisplayName("Name", typeof(Localisation.ViewModels.EditProduct))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(Localisation.ViewModels.EditProduct))]
        [StringLength(255, ErrorMessageResourceName = "NameLength", ErrorMessageResourceType = typeof(Localisation.ViewModels.EditProduct))]
        public string Name { get; set; }
        
        [LocalisedDisplayName("Description", typeof(Localisation.ViewModels.EditProduct))]
        public string Description { get; set; }
        
        [LocalisedDisplayName("Sku", typeof(Localisation.ViewModels.EditProduct))]
        [StringLength(100, ErrorMessageResourceName="SkuLength", ErrorMessageResourceType = typeof(Localisation.ViewModels.EditProduct))]
        public string Sku { get; set; }
        
        [LocalisedDisplayName("Price", typeof(Localisation.ViewModels.EditProduct))]
        public ICollection<EditPrice> Prices { get; set; }
        
        public bool IsDeleted { get; set; }
        
        [LocalisedDisplayName("Categories", typeof(Localisation.ViewModels.EditProduct))]
        public ICollection<EditCategory> Categories { get; set; }

        public void LoadFromEntity(Product productEntity)
        {
            Id = productEntity.Id;
            Name = productEntity.Name;
            Description = productEntity.Name;
            Sku = productEntity.Sku;
            Prices = productEntity.Prices.Select(x => new EditPrice { Id = x.Id, Value = x.Value, IsTaxIncluded = x.IsTaxIncluded, TaxRate = x.TaxRate }).ToList(); // need to figure out automapper asap
            IsDeleted = productEntity.IsDeleted;
            Categories =
                productEntity.Categories.Select(
                    x => new EditCategory() {Id = x.Id, Name = x.Name, Description = x.Description}).ToList();
        }

        public IEnumerable<ErrorInfo> Errors()
        {
            yield return null;
        }

        public bool IsValid()
        {
            return !Errors().Any();
        }
    }
}