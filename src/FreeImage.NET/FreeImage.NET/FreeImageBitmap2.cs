using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace FreeImageAPI
{
	public class FreeImageBitmap2 : FreeImageBitmapBase
	{
		#region Fields

		IPixelValueIO _pixelIO;

		#endregion

		#region Properties

		public bool IsRGBA { get { return this.PixelValueIO.IsRGBA; } }

		protected IPixelValueIO PixelValueIO
		{
			get
			{
				if (this._pixelIO == null)
				{
					this._pixelIO = FreeImageBitmap2.GetPixelValueIO(this);
					if (this._pixelIO == null)
						throw new NotSupportedException("The underlying bitmap type is not supported.");
				}
				return this._pixelIO;
			}
		}

		#endregion

		#region Constructors

		/// <inheritdoc cref="FreeImageBitmapBase.#ctor(FreeImageBitmapBase)"/>
		public FreeImageBitmap2(FreeImageBitmap2 bitmap)
			: base(bitmap) { }

		/// <inheritdoc cref="FreeImageBitmapBase.#ctor(Stream,FREE_IMAGE_FORMAT,FREE_IMAGE_LOAD_FLAGS)"/>
		public FreeImageBitmap2(Stream stream, FREE_IMAGE_FORMAT format = FREE_IMAGE_FORMAT.FIF_UNKNOWN, FREE_IMAGE_LOAD_FLAGS loadFlags = FREE_IMAGE_LOAD_FLAGS.DEFAULT)
			: base(stream, format, loadFlags) { }

		/// <inheritdoc cref="FreeImageBitmapBase.#ctor(string,FREE_IMAGE_FORMAT,FREE_IMAGE_LOAD_FLAGS)"/>
		public FreeImageBitmap2(string filename, FREE_IMAGE_FORMAT format = FREE_IMAGE_FORMAT.FIF_UNKNOWN, FREE_IMAGE_LOAD_FLAGS loadFlags = FREE_IMAGE_LOAD_FLAGS.DEFAULT)
			: base(filename, format, loadFlags) { }

		/// <summary>
		/// Creates a new Bitmap in Memory.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="bpp"></param>
		/// <param name="imageType"></param>
		/// <param name="redMask"></param>
		/// <param name="greenMask"></param>
		/// <param name="blueMask"></param>
		public FreeImageBitmap2(int width, int height, int bpp = 32, FREE_IMAGE_TYPE imageType = FREE_IMAGE_TYPE.FIT_BITMAP, uint redMask = 0, uint greenMask = 0, uint blueMask = 0)
			:base()
		{
			this.dib = FreeImage.AllocateT(imageType, width, height, bpp, redMask, greenMask, blueMask);
			if (dib.IsNull)
			{
				throw new Exception(ErrorCreatingBitmap);
			}
			AddMemoryPressure();
		}
		#endregion

		#region Public Methods

		public IPixelValue GetPixel(int x, int y)
		{
			return this.PixelValueIO.GetValue(x, y);
		}

		public void SetPixel(int x, int y, IPixelValue value)
		{
			this.PixelValueIO.SetValue(x, y, value);
		}

		public RGBAColor GetPixelRGBA(int x, int y)
		{
			IPixelValue value = this.GetPixel(x, y);
			return this.PixelValueIO.ConvertToRGBA(value);
		}

		public void SetPixelRGBA(int x, int y, RGBAColor color)
		{
			IPixelValue value = this.PixelValueIO.ConvertFrom(color);
			this.SetPixel(x, y, value);
		}

		public FreeImageBitmap2 GetEmptyCopy(bool supportTransparency = false)
		{
			int bpp = this.BitsPerPixel;
			FREE_IMAGE_TYPE imageType = this.ImageType;
			uint r, g, b;
			if (supportTransparency && this.IsRGBA && !this.IsTransparent)
			{
				r = g = b = 0;
				if (bpp <= 32)
				{
					bpp = 32;
					imageType = FREE_IMAGE_TYPE.FIT_BITMAP;
				}
				else if (bpp <= 64)
				{
					bpp = 64;
					imageType = FREE_IMAGE_TYPE.FIT_RGBA16;
				}
				else if (bpp <= 128)
				{
					bpp = 128;
					imageType = FREE_IMAGE_TYPE.FIT_RGBAF;
				}
			}
			else
			{
				r = this.RedMask;
				g = this.GreenMask;
				b = this.BlueMask;
			}
			FreeImageBitmap2 RetVal = new FreeImageBitmap2(this.Width, this.Height, bpp, imageType, r, g, b);
			if (bpp <= 32)
				RetVal.IsTransparent = true;
			return RetVal;
		}

		public bool FillBackground(IPixelValue background, FREE_IMAGE_COLOR_OPTIONS colorOptions = FREE_IMAGE_COLOR_OPTIONS.DEFAULT)
		{
			this.EnsureNotDisposed();
			GCHandle handle = new GCHandle();
			try
			{
				handle = GCHandle.Alloc(background, GCHandleType.Pinned);
				return FreeImage.FillBackground(this.dib, handle.AddrOfPinnedObject(), colorOptions);
			}
			finally
			{
				if (handle.IsAllocated)
					handle.Free();
			}
		}

		public bool FillBackgroundRGBA(RGBAColor background, FREE_IMAGE_COLOR_OPTIONS colorOptions = FREE_IMAGE_COLOR_OPTIONS.DEFAULT)
		{
			IPixelValue value = this.PixelValueIO.ConvertFrom(background);
			return this.FillBackground(value, colorOptions);
		}

		public RGBAColor GetDominantColorRGBA(double maxDeviation = 0.1)
		{
			return this.GetDominantColorRGBA(new RGBAColor(maxDeviation));
		}

		public RGBAColor GetDominantColorRGBA(RGBAColor maxDeviation)
		{
			RGBAColor minValue = new RGBAColor(0d);
			RGBAColor maxValue = new RGBAColor(1d);

			RGBAColor average;
			RGBAColor stdDev = this.GetStandardDeviationRGBA(minValue, maxValue, out average);

			while (stdDev.R > maxDeviation.R || stdDev.G > maxDeviation.G || stdDev.B > maxDeviation.B || stdDev.A > maxDeviation.A)
			{
				minValue.R = Math.Max(0d, average.R - stdDev.R);
				minValue.G = Math.Max(0d, average.G - stdDev.G);
				minValue.B = Math.Max(0d, average.B - stdDev.B);
				minValue.A = Math.Max(0d, average.A - stdDev.A);

				maxValue.R = Math.Min(1d, average.R + stdDev.R);
				maxValue.G = Math.Min(1d, average.G + stdDev.G);
				maxValue.B = Math.Min(1d, average.B + stdDev.B);
				maxValue.A = Math.Min(1d, average.A + stdDev.A);

				stdDev = this.GetStandardDeviationRGBA(minValue, maxValue, out average);
			}

			return average;
		}

		public RGBAColor GetAverageColorRGBA()
		{
			return this.GetAverageColorRGBA(0d, 1d);
		}

		public RGBAColor GetAverageColorRGBA(double minValue, double maxValue)
		{
			return this.GetAverageColorRGBA(new RGBAColor(minValue), new RGBAColor(maxValue));
		}

		public RGBAColor GetAverageColorRGBA(RGBAColor minValue, RGBAColor maxValue)
		{
			double rtot = 0;
			double gtot = 0;
			double btot = 0;
			double atot = 0;
			long ctot = 0;
			object _lock = new object();

			int height = this.Height;
			int width = this.Width;

			Parallel.For(0, height, (y) =>
				{
					double r = 0;
					double g = 0;
					double b = 0;
					double a = 0;
					long counter = 0;
					for (int x = 0; x < width; x++)
					{
						RGBAColor val = this.GetPixelRGBA(x, y);
						if (val.R < minValue.R || val.R > maxValue.R)
							continue;
						if (val.G < minValue.G || val.G > maxValue.G)
							continue;
						if (val.B < minValue.B || val.B > maxValue.B)
							continue;
						if (val.A < minValue.A || val.A > maxValue.A)
							continue;

						counter++;
						r += val.R;
						g += val.G;
						b += val.B;
						a += val.A;
					}

					lock(_lock)
					{
						ctot += counter;
						rtot += r;
						gtot += g;
						btot += b;
						atot += a;
					}
				});

			if (ctot < 1)
			{
				throw new ArgumentException("No pixelvalues between minValue and maxValue");
			}

			RGBAColor average = new RGBAColor();
			average.R = rtot / ctot;
			average.G = gtot / ctot;
			average.B = btot / ctot;
			average.A = atot / ctot;
			return average;
		}

		public RGBAColor GetStandardDeviationRGBA(RGBAColor average)
		{
			return this.GetStandardDeviationRGBA(0, 1, average);
		}

		public RGBAColor GetStandardDeviationRGBA(double minValue, double maxValue, RGBAColor average)
		{
			return this.GetStandardDeviationRGBA(new RGBAColor(minValue), new RGBAColor(maxValue), average);
		}

		public RGBAColor GetStandardDeviationRGBA(RGBAColor minValue, RGBAColor maxValue, RGBAColor average)
		{
			double rtot = 0;
			double gtot = 0;
			double btot = 0;
			double atot = 0;
			long ctot = 0;
			object _lock = new object();

			int height = this.Height;
			int width = this.Width;

			Parallel.For(0, height, (y) =>
			{
				double r = 0;
				double g = 0;
				double b = 0;
				double a = 0;
				long counter = 0;
				for (int x = 0; x < width; x++)
				{
					RGBAColor val = this.GetPixelRGBA(x, y);
					if (val.R < minValue.R || val.R > maxValue.R)
						continue;
					if (val.G < minValue.G || val.G > maxValue.G)
						continue;
					if (val.B < minValue.B || val.B > maxValue.B)
						continue;
					if (val.A < minValue.A || val.A > maxValue.A)
						continue;

					counter++;
					double d = val.R - average.R;
					r += d * d;

					d = val.G - average.G;
					g += d * d;

					d = val.B - average.B;
					b += d * d;

					d = val.A - average.A;
					a += d * d;
				}

				lock (_lock)
				{
					ctot += counter;
					rtot += r;
					gtot += g;
					btot += b;
					atot += a;
				}
			});

			if (ctot < 1)
			{
				throw new ArgumentException("No pixelvalues between minValue and maxValue");
			}

			RGBAColor stdDeviation = new RGBAColor();
			stdDeviation.R = Math.Sqrt(rtot / ctot);
			stdDeviation.G = Math.Sqrt(gtot / ctot);
			stdDeviation.B = Math.Sqrt(btot / ctot);
			stdDeviation.A = Math.Sqrt(atot / ctot);
			return stdDeviation;
		}

		public RGBAColor GetStandardDeviationRGBA(out RGBAColor average)
		{
			return this.GetStandardDeviationRGBA(0, 1, out average);
		}

		public RGBAColor GetStandardDeviationRGBA(double minValue, double maxValue, out RGBAColor average)
		{
			return this.GetStandardDeviationRGBA(new RGBAColor(minValue), new RGBAColor(maxValue), out average);
		}

		public RGBAColor GetStandardDeviationRGBA(RGBAColor minValue, RGBAColor maxValue, out RGBAColor average)
		{
			average = this.GetAverageColorRGBA(minValue, maxValue);
			return this.GetStandardDeviationRGBA(minValue, maxValue, average);
		}

		public FreeImageBitmap2 RemoveBackground(RGBAColor background, double tolerance = 0.0)
		{
			if (!this.IsRGBA)
				throw new InvalidOperationException("Bitmap.IsRGBA must return true.");

			FreeImageBitmap2 RetVal = this.GetEmptyCopy(true);
			if (!RetVal.IsTransparent)
				throw new InvalidOperationException("Bitmap which supports transparency could not be created.");

			int height = this.Height;
			int width = this.Width;

			Parallel.For(0, height, (y) =>
			{
				for (int x = 0; x < width; x++)
				{
					RGBAColor val = this.GetPixelRGBA(x, y);
					double a_b = background.A;
					double a = val.A;
					if (a_b > a)
					{
						throw new ArgumentOutOfRangeException("background", string.Format("background.A is greater than Bitmap.GetPixelRGBA({0},{1}).A -> No physical solution.", x, y));
					}

					RGBAColor foreground = new RGBAColor();

					if (a_b < 1)
					{
						double a_f = (a - a_b) / (1 - a_b);
						double a_p = a / a_f;
						double a_m = (1 - 1 / a_f) * a_b;
						foreground.A = a_f;
						foreground.R = (val.R * a_p) + (a_m * background.R);
						foreground.G = (val.G * a_p) + (a_m * background.G);
						foreground.B = (val.B * a_p) + (a_m * background.B);
					}
					else
					{
						double a_f1 = this.RemoveBackground_GetAlpha(val.R, background.R, tolerance);
						double a_f2 = this.RemoveBackground_GetAlpha(val.G, background.G, tolerance);
						double a_f3 = this.RemoveBackground_GetAlpha(val.B, background.B, tolerance);
						double a_f = Math.Max(a_f1, Math.Max(a_f2, a_f3));
						foreground.A = a_f;
						foreground.R = this.RemoveBackground_GetForegroundColor(val.R, background.R, a_f);
						foreground.G = this.RemoveBackground_GetForegroundColor(val.G, background.G, a_f);
						foreground.B = this.RemoveBackground_GetForegroundColor(val.B, background.B, a_f);
					}
					RetVal.SetPixelRGBA(x, y, foreground);
				}
			});
			return RetVal;
		}

		double RemoveBackground_GetAlpha(double c, double c_b, double tolerance)
		{
			double a = c - c_b;
			if (Math.Abs(a) <= tolerance)
				return 0d;

			if (a < 0)
				a = a / (-c_b);
			else
				a = a / (1d - c_b);
			return Math.Max(0d, Math.Min(a, 1d));
		}

		double RemoveBackground_GetForegroundColor(double c, double c_b, double a)
		{
			if (a <= 0)
				return 0;
			double c_f = c_b + (c - c_b) / a;
			return Math.Max(0d, Math.Min(1d, c_f));
		}

		#endregion

		#region Static Methods

		static IPixelValueIO GetPixelValueIO(FreeImageBitmap2 bitmap)
		{
			FREE_IMAGE_TYPE imType = bitmap.ImageType;
			if (imType == FREE_IMAGE_TYPE.FIT_BITMAP)
			{
				int bpp = bitmap.BitsPerPixel;
				if (bpp == 16)
				{
					if (bitmap.GreenMask == FreeImage.FI16_555_GREEN_MASK)
						return new PixelValueIO<FI16RGB555>(bitmap);
					else
						return new PixelValueIO<FI16RGB565>(bitmap);
				}
				else if (bpp ==24)
				{
					return new PixelValueIO<RGBTRIPLE>(bitmap);
				}
				else if (bpp == 32)
				{
					return new PixelValueIO<RGBQUAD>(bitmap);
				}
			}
			else if (imType == FREE_IMAGE_TYPE.FIT_RGB16)
			{
				return new PixelValueIO<FIRGB16>(bitmap);
			}
			else if (imType == FREE_IMAGE_TYPE.FIT_RGBA16)
			{
				return new PixelValueIO<FIRGBA16>(bitmap);
			}
			else if(imType == FREE_IMAGE_TYPE.FIT_RGBF)
			{
				return new PixelValueIO<FIRGBF>(bitmap);
			}
			else if(imType == FREE_IMAGE_TYPE.FIT_RGBAF)
			{
				return new PixelValueIO<FIRGBAF>(bitmap);
			}
			return null;
		}

		#endregion

		#region Subtypes

		#endregion
	}
}
