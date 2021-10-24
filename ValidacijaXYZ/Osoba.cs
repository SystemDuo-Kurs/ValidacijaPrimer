using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacijaXYZ
{
	public class Osoba : INotifyPropertyChanged, IDataErrorInfo
	{
		private string _ime;
		public string Ime 
		{ 
			get => _ime;
			set
			{
				_ime = value;
				Izmena("Ime");
			}
		}
		private string _prezime;
		public string Prezime 
		{ 
			get => _prezime; 
			set
			{
				_prezime = value;
				Izmena("Prezime");
			}
		}
		private string _mejl;
		public string Mejl 
		{ 
			get => _mejl; 
			set
			{
				_mejl = value;
				Izmena("Mejl");
			}
		}
		private int _godiste;
		public int Godiste 
		{ 
			get => _godiste; 
			set
			{
				_godiste = value;
				Izmena("Godiste");
			}
		}
		private OsobaValidator _validator = new();

		public string this[string nazivPropertija]
		{
			get
			{
				var rez = _validator.Validate(this);
				var greska = 
					rez.Errors.Where(err => err.PropertyName == nazivPropertija)
							  .FirstOrDefault();
				if (greska is not null)
					return greska.ErrorMessage;
				return string.Empty;
			}
		}

		public void Izmena(string nazivPropertija)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nazivPropertija));

		public event PropertyChangedEventHandler PropertyChanged;

		public string Error => throw new NotImplementedException();
	}

	public class OsobaValidator : AbstractValidator<Osoba>
	{
		public OsobaValidator()
		{
			RuleFor(o => o.Ime).NotEmpty().WithMessage("Jok prazno")
				.MinimumLength(3).WithMessage("Jok premalo");
			RuleFor(o => o.Prezime).NotEmpty().WithMessage("Jok prazno")
				.MinimumLength(3).WithMessage("Jok premalo");
			RuleFor(o => o.Mejl).NotEmpty().WithMessage("Jok prazno")
				.EmailAddress();
			RuleFor(o => o.Godiste).InclusiveBetween(1900, 2050).WithMessage("Jok jok");
		}
	}
}
