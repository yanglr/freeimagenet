using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeImageAPI
{
	public interface IPixelValue
	{
	}

	public interface IRGBAColor : IPixelValue
	{
		double R { get; set; }
		double G { get; set; }
		double B { get; set; }
		double A { get; set; }
	}

	public struct RGBAColor
	{
		const uint b32 = 0xFFFFFFFF;

		public RGBAColor(double valForAll)
			:this()
		{
			this.R = valForAll;
			this.G = valForAll;
			this.B = valForAll;
			this.A = valForAll;
		}

		double _r;
		double _g;
		double _b;
		double _a;

		public double R
		{
			get { return this._r; }
			set
			{
				if (value < 0d || value > 1d)
					throw new ArgumentOutOfRangeException();
				this._r = value;
			}
		}

		public double G
		{
			get { return this._g; }
			set
			{
				if (value < 0d || value > 1d)
					throw new ArgumentOutOfRangeException();
				this._g = value;
			}
		}

		public double B
		{
			get { return this._b; }
			set
			{
				if (value < 0d || value > 1d)
					throw new ArgumentOutOfRangeException();
				this._b = value;
			}
		}

		public double A
		{
			get { return this._a; }
			set
			{
				if (value < 0d || value > 1d)
					throw new ArgumentOutOfRangeException();
				this._a = value;
			}
		}

		public override string ToString()
		{
			return string.Format("R={0:f4}, G={1:f4}, B={2:f4}, A={3:f4}", this.R, this.G, this.B, this.A);
		}

		//int IRGBAColor.R { get { return (int)(b32 * this.R); } set { this.R = (double)value / b32; } }
		//int IRGBAColor.G { get { return (int)(b32 * this.G); } set { this.G = (double)value / b32; } }
		//int IRGBAColor.B { get { return (int)(b32 * this.B); } set { this.B = (double)value / b32; } }
		//int IRGBAColor.A { get { return (int)(b32 * this.A); } set { this.A = (double)value / b32; } }

		//double IRGBAColor.NormR { get { return this.R; } set { this.R = value; } }
		//double IRGBAColor.NormG { get { return this.G; } set { this.G = value; } }
		//double IRGBAColor.NormB { get { return this.B; } set { this.B = value; } }
		//double IRGBAColor.NormA { get { return this.A; } set { this.A = value; } }
	}
}
