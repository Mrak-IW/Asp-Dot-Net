using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab8.Models.MathSolvers
{
	public class ComplexNumber
	{
		double Re { get; set; }
		double Im { get; set; }

		public ComplexNumber(double real, double imaginary)
		{
			Re = real;
			Im = imaginary;
		}

		public ComplexNumber(double real)
			: this(real, 0)
		{ }

		public static ComplexNumber operator +(ComplexNumber val1, ComplexNumber val2)
		{
			ComplexNumber result = new ComplexNumber(val1.Re, val1.Im);
			result.Re += val2.Re;
			result.Im += val2.Im;
			return result;
		}

		public static ComplexNumber operator +(ComplexNumber val1, double val2)
		{
			ComplexNumber result = new ComplexNumber(val1.Re, val1.Im);
			result.Re += val2;
			return result;
		}

		public static ComplexNumber operator -(ComplexNumber val1, ComplexNumber val2)
		{
			ComplexNumber result = new ComplexNumber(val1.Re, val1.Im);
			result.Re -= val2.Re;
			result.Im -= val2.Im;
			return result;
		}

		public static ComplexNumber operator -(ComplexNumber val1, double val2)
		{
			ComplexNumber result = new ComplexNumber(val1.Re, val1.Im);
			result.Re -= val2;
			return result;
		}

		public static ComplexNumber operator *(ComplexNumber val1, ComplexNumber val2)
		{
			ComplexNumber result = new ComplexNumber(0, 0);
			result.Re = val1.Re * val2.Re - (val1.Im + val2.Im);
			result.Im = val1.Re * val2.Im + val1.Im * val2.Re;
			return result;
		}

		public static ComplexNumber operator *(ComplexNumber val1, double val2)
		{
			ComplexNumber result = new ComplexNumber(0, 0);
			result.Re = val1.Re * val2;
			result.Im = val1.Im * val2;
			return result;
		}

		public static ComplexNumber operator /(ComplexNumber val1, double val2)
		{
			ComplexNumber result = new ComplexNumber(0, 0);
			result.Re = val1.Re / val2;
			result.Im = val1.Im / val2;
			return result;
		}

		public override string ToString()
		{
			string result = "0";
			if (Re != 0)
			{
				result = Re.ToString();
			}
			if (Im != 0)
			{
				result = string.Format("{0} {1}i", result, Im > 0 ? "+ " + Im : "- " + Im * -1);
			}
			return result;
		}
	}
}