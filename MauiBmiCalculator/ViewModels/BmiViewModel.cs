using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiBmiCalculator.ViewModels
{
    public class BmiViewModel : INotifyPropertyChanged
    {
        private double _height;
        private double _weight;
        private double _bmi;
        private string _bmiCategory = string.Empty;
        private const double Tolerance = 0.0001;

        public double Height
        {
            get => _height;
            set
            {
                if (Math.Abs(_height - value) > Tolerance)
                {
                    _height = value;
                    OnPropertyChanged();
                    CalculateBmi();
                }
            }
        }

        public double Weight
        {
            get => _weight;
            set
            {
                if (Math.Abs(_weight - value) > Tolerance)
                {
                    _weight = value;
                    OnPropertyChanged();
                    CalculateBmi();
                }
            }
        }

        public double Bmi
        {
            get => _bmi;
        }

        public string BmiCategory
        {
            get => _bmiCategory;
            private set
            {
                if (_bmiCategory != value)
                {
                    _bmiCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CalculateBmi()
        {
            if (Height > 0)
            {
                // Convert height from cm to meters for BMI calculation
                double heightInMeters = Height >= 3 ? Height / 100.0 : Height;
                double bmi = Weight / (heightInMeters * heightInMeters);
                if (Math.Abs(_bmi - bmi) > Tolerance)
                {
                    _bmi = bmi;
                    OnPropertyChanged(nameof(Bmi));
                }
                if (bmi < 18.5)
                    BmiCategory = "Underweight";
                else if (bmi < 25)
                    BmiCategory = "Normal weight";
                else if (bmi < 30)
                    BmiCategory = "Overweight";
                else
                    BmiCategory = "Obese";
            }
            else
            {
                BmiCategory = "Invalid input";
            }
        }
    }
}