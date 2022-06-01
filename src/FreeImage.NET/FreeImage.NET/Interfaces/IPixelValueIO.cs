using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace FreeImageAPI
{
	public interface IPixelValueIO
	{
		IPixelValue GetValue(int x, int y);
		void SetValue(int x, int y, IPixelValue value);
		bool IsRGBA { get; }

		IPixelValue ConvertFrom(RGBAColor color);
		RGBAColor ConvertToRGBA(IPixelValue value);
	}

	public class PixelValueIO<T> : IPixelValueIO
		where T : IPixelValue, new()
	{
		protected int Width { get; private set; }
		protected int Height { get; private set; }
		protected int Pitch { get; private set; }
		protected long BaseAdress { get; private set; }
		protected int BPP { get; private set; }

		public bool IsRGBA { get { return typeof(IRGBAColor).IsAssignableFrom(typeof(T)); } }

		public PixelValueIO(FreeImageBitmap2 bitmap)
		{
			this.Width = bitmap.Width;
			this.Height = bitmap.Height;
			this.Pitch = bitmap.Pitch;
			this.BaseAdress = bitmap.Bits.ToInt64();
			this.BPP = bitmap.BitsPerPixel;
		}

		protected IntPtr GetPixelAdress(int x, int y)
		{
			if (x < 0 || x >= this.Width)
				throw new ArgumentOutOfRangeException("x");
			if (y < 0 || y >= this.Height)
				throw new ArgumentOutOfRangeException("y");
			y = this.Height - y - 1;
			long offset = (long)y * this.Pitch + (long)x * (this.BPP / 8);
			return new IntPtr(offset + this.BaseAdress);
		}

		public virtual IPixelValue ConvertFrom(RGBAColor value)
		{
			IRGBAColor color = (IRGBAColor)(new T());
			color.R = value.R;
			color.G = value.G;
			color.B = value.B;
			color.A = value.A;
			return color;
		}

		public virtual RGBAColor ConvertToRGBA(IPixelValue value)
		{
			IRGBAColor color = (IRGBAColor)value;
			return new RGBAColor() { R = color.R, G = color.G, B = color.B, A = color.A };
		}

		public virtual T GetValue(int x, int y)
		{
			IntPtr ptr = this.GetPixelAdress(x, y);
			return (T)Marshal.PtrToStructure(ptr, typeof(T));
		}

		public virtual void SetValue(int x, int y, T value)
		{
			IntPtr ptr = this.GetPixelAdress(x, y);
			Marshal.StructureToPtr(value, ptr, false);
		}

		IPixelValue IPixelValueIO.GetValue(int x, int y) { return this.GetValue(x, y); }
		void IPixelValueIO.SetValue(int x, int y, IPixelValue value) { this.SetValue(x, y, (T)value); }
	}
}
