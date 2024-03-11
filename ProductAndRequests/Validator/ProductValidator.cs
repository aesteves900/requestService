using FluentValidation;
using ProductAndRequests.Models;

namespace ProductAndRequests.Validator
{


    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(2, 100);
            RuleFor(x => x.Price).GreaterThan(0).LessThan(10000);            
        }
    }

}
