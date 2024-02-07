using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Shop_Bear.Models.Common
{
    public class EmailEntity
    {
        public string FromEmailAddress {  get; set; }
        public string ToEmailAddress { get; set; }
        public string Subject {  get; set; }
        public string EmailBodyMessage {  get; set; }
        [ValidateNever]
        public string AttacmentUrl {  get; set; }
    }
}
