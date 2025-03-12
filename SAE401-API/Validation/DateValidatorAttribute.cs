using System.ComponentModel.DataAnnotations;

namespace SAE401_API.Validation


{
    public class DateValidatorAttribute : ValidationAttribute
    {
        public DateValidatorAttribute()
        {
            this.ErrorMessage = "La date ne peut pas être dans le futur.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // Accepter les valeurs nulles si besoin
            }

            if (value is DateTime date)
            {
                return date <= DateTime.Now; // Vérifie que la date n'est pas dans le futur
            }

            return false; // Si ce n'est pas une DateTime valide, on retourne false
        }

    }
}
