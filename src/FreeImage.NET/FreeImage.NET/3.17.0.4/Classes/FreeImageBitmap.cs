// ==========================================================
// FreeImage 3 .NET wrapper
// Original FreeImage 3 functions and .NET compatible derived functions
//
// Design and implementation by
// - Jean-Philippe Goerke (jpgoerke@users.sourceforge.net)
// - Carsten Klein (cklein05@users.sourceforge.net)
//
// Contributors:
// - David Boland (davidboland@vodafone.ie)
//
// Main reference : MSDN Knowlede Base
//
// This file is part of FreeImage 3
//
// COVERED CODE IS PROVIDED UNDER THIS LICENSE ON AN "AS IS" BASIS, WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, WITHOUT LIMITATION, WARRANTIES
// THAT THE COVERED CODE IS FREE OF DEFECTS, MERCHANTABLE, FIT FOR A PARTICULAR PURPOSE
// OR NON-INFRINGING. THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE COVERED
// CODE IS WITH YOU. SHOULD ANY COVERED CODE PROVE DEFECTIVE IN ANY RESPECT, YOU (NOT
// THE INITIAL DEVELOPER OR ANY OTHER CONTRIBUTOR) ASSUME THE COST OF ANY NECESSARY
// SERVICING, REPAIR OR CORRECTION. THIS DISCLAIMER OF WARRANTY CONSTITUTES AN ESSENTIAL
// PART OF THIS LICENSE. NO USE OF ANY COVERED CODE IS AUTHORIZED HEREUNDER EXCEPT UNDER
// THIS DISCLAIMER.
//
// Use at your own risk!
// ==========================================================

// ==========================================================
// CVS
// $Revision: 1.12 $
// $Date: 2011/12/22 14:54:22 $
// $Id: FreeImageBitmap.cs,v 1.12 2011/12/22 14:54:22 drolon Exp $
// ==========================================================

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using FreeImageAPI.Metadata;
using System.Diagnostics;

namespace FreeImageAPI
{
	/// <summary>
	/// Encapsulates a FreeImage-bitmap.
	/// </summary>
	[Serializable, Guid("64a4c935-b757-499c-ab8c-6110316a9e51")]
	public partial class FreeImageBitmap : FreeImageBitmapBase
	{
		#region Constructors and Destructor

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class.
		/// </summary>
		protected FreeImageBitmap()
			:base(){}

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class.
		/// For internal use only.
		/// </summary>
		/// <exception cref="Exception">The operation failed.</exception>
		internal protected FreeImageBitmap(FIBITMAP dib)
			:base(dib){}

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified image.
		/// </summary>
		/// <param name="original">The original to clone from.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="original"/> is a null reference.</exception>
		public FreeImageBitmap(FreeImageBitmap original)
			:base(original){}

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified image with the specified size.
		/// </summary>
		/// <param name="original">The original to clone from.</param>
		/// <param name="newSize">The Size structure that represent the
		/// size of the new <see cref="FreeImageBitmap"/>.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="original"/> is a null reference.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="newSize.Width"/> or <paramref name="newSize.Height"/> are less or equal zero.
		/// </exception>
		public FreeImageBitmap(FreeImageBitmap original, Size newSize)
			:base(original,newSize){}

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified image with the specified size.
		/// </summary>
		/// <param name="original">The original to clone from.</param>
		/// <param name="width">Width of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="height">Height of the new <see cref="FreeImageBitmap"/>.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="original"/> is a null reference.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="width"/> or <paramref name="height"/> are less or equal zero.</exception>
		public FreeImageBitmap(FreeImageBitmap original, int width, int height)
			:base(original,width,height){}

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified image.
		/// </summary>
		/// <param name="original">The original to clone from.</param>
		/// <remarks>
		/// Although this constructor supports creating images in both formats
		/// <see cref="System.Drawing.Imaging.PixelFormat.Format32bppPArgb"/>
		/// and <see cref="System.Drawing.Imaging.PixelFormat.Format64bppPArgb"/>, bitmaps
		/// created in these formats are treated like any normal 32-bit RGBA and 64-bit RGBA
		/// images respectively. Currently, there is no  support for automatic premultiplying images in
		/// <see cref="FreeImageBitmap"/>.
		/// </remarks>
		/// <exception cref="Exception">The operation failed.</exception>
		public FreeImageBitmap(Image original)
			:base(original){}

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified image with the specified size.
		/// </summary>
		/// <param name="original">The original to clone from.</param>
		/// <param name="newSize">The Size structure that represent the
		/// size of the new <see cref="FreeImageBitmap"/>.</param>
		/// <remarks>
		/// Although this constructor supports creating images in both formats
		/// <see cref="System.Drawing.Imaging.PixelFormat.Format32bppPArgb"/>
		/// and <see cref="System.Drawing.Imaging.PixelFormat.Format64bppPArgb"/>, bitmaps
		/// created in these formats are treated like any normal 32-bit RGBA and 64-bit RGBA
		/// images respectively. Currently, there is no  support for automatic premultiplying images in
		/// <see cref="FreeImageBitmap"/>.
		/// </remarks>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="original"/> is a null reference.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="newSize.Width"/> or <paramref name="newSize.Height"/> are less or equal zero.
		/// </exception>
		public FreeImageBitmap(Image original, Size newSize)
			:base(original,newSize){}

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified image with the specified size.
		/// </summary>
		/// <param name="original">The original to clone from.</param>
		/// <param name="width">The width, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <remarks>
		/// Although this constructor supports creating images in both formats
		/// <see cref="System.Drawing.Imaging.PixelFormat.Format32bppPArgb"/>
		/// and <see cref="System.Drawing.Imaging.PixelFormat.Format64bppPArgb"/>, bitmaps
		/// created in these formats are treated like any normal 32-bit RGBA and 64-bit RGBA
		/// images respectively. Currently, there is no  support for automatic premultiplying images in
		/// <see cref="FreeImageBitmap"/>.
		/// </remarks>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="original"/> is a null reference.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="width"/> or <paramref name="height"/> are less or equal zero.</exception>
		public FreeImageBitmap(Image original, int width, int height)
			: base(original, width, height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified image.
		/// </summary>
		/// <param name="original">The original to clone from.</param>
		/// <remarks>
		/// Although this constructor supports creating images in both formats
		/// <see cref="System.Drawing.Imaging.PixelFormat.Format32bppPArgb"/>
		/// and <see cref="System.Drawing.Imaging.PixelFormat.Format64bppPArgb"/>, bitmaps
		/// created in these formats are treated like any normal 32-bit RGBA and 64-bit RGBA
		/// images respectively. Currently, there is no  support for automatic premultiplying images in
		/// <see cref="FreeImageBitmap"/>.
		/// </remarks>
		/// <exception cref="ArgumentNullException"><paramref name="original"/> is a null reference.</exception>
		/// <exception cref="Exception">The operation failed.</exception>
		public FreeImageBitmap(Bitmap original)
			: base(original) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified image with the specified size.
		/// </summary>
		/// <param name="original">The original to clone from.</param>
		/// <param name="newSize">The Size structure that represent the
		/// size of the new <see cref="FreeImageBitmap"/>.</param>
		/// <remarks>
		/// Although this constructor supports creating images in both formats
		/// <see cref="System.Drawing.Imaging.PixelFormat.Format32bppPArgb"/>
		/// and <see cref="System.Drawing.Imaging.PixelFormat.Format64bppPArgb"/>, bitmaps
		/// created in these formats are treated like any normal 32-bit RGBA and 64-bit RGBA
		/// images respectively. Currently, there is no  support for automatic premultiplying images in
		/// <see cref="FreeImageBitmap"/>.
		/// </remarks>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="original"/> is a null reference.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="newSize.Width"/> or <paramref name="newSize.Height"/> are less or equal zero.
		/// </exception>
		public FreeImageBitmap(Bitmap original, Size newSize)
			: base(original, newSize) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified image with the specified size.
		/// </summary>
		/// <param name="original">The original to clone from.</param>
		/// <param name="width">The width, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <remarks>
		/// Although this constructor supports creating images in both formats
		/// <see cref="System.Drawing.Imaging.PixelFormat.Format32bppPArgb"/>
		/// and <see cref="System.Drawing.Imaging.PixelFormat.Format64bppPArgb"/>, bitmaps
		/// created in these formats are treated like any normal 32-bit RGBA and 64-bit RGBA
		/// images respectively. Currently, there is no  support for automatic premultiplying images in
		/// <see cref="FreeImageBitmap"/>.
		/// </remarks>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="original"/> is a null reference.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="width"/> or <paramref name="height"/> are less or equal zero.</exception>
		public FreeImageBitmap(Bitmap original, int width, int height)
			: base(original, width, height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified stream.
		/// </summary>
		/// <param name="stream">Stream to read from.</param>
		/// <param name="useIcm">Ignored.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is a null reference.</exception>
		/// <remarks>
		/// You must keep the stream open for the lifetime of the <see cref="FreeImageBitmap"/>.
		/// </remarks>
		public FreeImageBitmap(Stream stream, bool useIcm)
			: base(stream, useIcm) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified stream.
		/// </summary>
		/// <param name="stream">Stream to read from.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is a null reference.</exception>
		/// <remarks>
		/// You must keep the stream open for the lifetime of the <see cref="FreeImageBitmap"/>.
		/// </remarks>
		public FreeImageBitmap(Stream stream)
			: base(stream) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified stream in the specified format.
		/// </summary>
		/// <param name="stream">Stream to read from.</param>
		/// <param name="format">Format of the image.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is a null reference.</exception>
		/// <remarks>
		/// You must keep the stream open for the lifetime of the <see cref="FreeImageBitmap"/>.
		/// </remarks>
		public FreeImageBitmap(Stream stream, FREE_IMAGE_FORMAT format)
			: base(stream, format) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified stream with the specified loading flags.
		/// </summary>
		/// <param name="stream">Stream to read from.</param>
		/// <param name="flags">Flags to enable or disable plugin-features.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is a null reference.</exception>
		/// <remarks>
		/// You must keep the stream open for the lifetime of the <see cref="FreeImageBitmap"/>.
		/// </remarks>
		public FreeImageBitmap(Stream stream, FREE_IMAGE_LOAD_FLAGS flags)
			: base(stream, flags) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified stream in the specified format
		/// with the specified loading flags.
		/// </summary>
		/// <param name="stream">Stream to read from.</param>
		/// <param name="format">Format of the image.</param>
		/// <param name="flags">Flags to enable or disable plugin-features.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is a null reference.</exception>
		/// <remarks>
		/// You must keep the stream open for the lifetime of the <see cref="FreeImageBitmap"/>.
		/// </remarks>
		public FreeImageBitmap(Stream stream, FREE_IMAGE_FORMAT format, FREE_IMAGE_LOAD_FLAGS flags)
			: base(stream, format, flags) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified file.
		/// </summary>
		/// <param name="filename">The complete name of the file to load.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="filename"/> is a null reference.</exception>
		/// <exception cref="FileNotFoundException"><paramref name="filename"/> does not exist.</exception>
		public FreeImageBitmap(string filename)
			: base(filename) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified file.
		/// </summary>
		/// <param name="filename">The complete name of the file to load.</param>
		/// <param name="useIcm">Ignored.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="filename"/> is a null reference.</exception>
		/// <exception cref="FileNotFoundException"><paramref name="filename"/> does not exist.</exception>
		public FreeImageBitmap(string filename, bool useIcm)
			: base(filename, useIcm) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified file
		/// with the specified loading flags.
		/// </summary>
		/// <param name="filename">The complete name of the file to load.</param>
		/// <param name="flags">Flags to enable or disable plugin-features.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="filename"/> is a null reference.</exception>
		/// <exception cref="FileNotFoundException"><paramref name="filename"/> does not exist.</exception>
		public FreeImageBitmap(string filename, FREE_IMAGE_LOAD_FLAGS flags)
			: base(filename, flags) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified file
		/// in the specified format.
		/// </summary>
		/// <param name="filename">The complete name of the file to load.</param>
		/// <param name="format">Format of the image.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="filename"/> is a null reference.</exception>
		/// <exception cref="FileNotFoundException"><paramref name="filename"/> does not exist.</exception>
		public FreeImageBitmap(string filename, FREE_IMAGE_FORMAT format)
			: base(filename, format) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified file
		/// in the specified format with the specified loading flags.
		/// </summary>
		/// <param name="filename">The complete name of the file to load.</param>
		/// <param name="format">Format of the image.</param>
		/// <param name="flags">Flags to enable or disable plugin-features.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="filename"/> is a null reference.</exception>
		/// <exception cref="FileNotFoundException"><paramref name="filename"/> does not exist.</exception>
		public FreeImageBitmap(string filename, FREE_IMAGE_FORMAT format, FREE_IMAGE_LOAD_FLAGS flags)
			: base(filename, format, flags) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class
		/// bases on the specified size.
		/// </summary>
		/// <param name="width">The width, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		public FreeImageBitmap(int width, int height)
			: base(width, height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified resource.
		/// </summary>
		/// <param name="type">The class used to extract the resource.</param>
		/// <param name="resource">The name of the resource.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		public FreeImageBitmap(Type type, string resource)
			: base(type, resource) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified size
		/// and with the resolution of the specified <see cref="System.Drawing.Graphics"/> object.
		/// </summary>
		/// <param name="width">The width, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="g">The Graphics object that specifies the resolution for the new <see cref="FreeImageBitmap"/>.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="g"/> is a null reference.</exception>
		public FreeImageBitmap(int width, int height, Graphics g)
			: base(width, height, g) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified size and format.
		/// </summary>
		/// <param name="width">The width, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="format">The PixelFormat enumeration for the new <see cref="FreeImageBitmap"/>.</param>
		/// <remarks>
		/// Although this constructor supports creating images in both formats
		/// <see cref="System.Drawing.Imaging.PixelFormat.Format32bppPArgb"/>
		/// and <see cref="System.Drawing.Imaging.PixelFormat.Format64bppPArgb"/>, bitmaps
		/// created in these formats are treated like any normal 32-bit RGBA and 64-bit RGBA
		/// images respectively. Currently, there is no  support for automatic premultiplying images in
		/// <see cref="FreeImageBitmap"/>.
		/// </remarks>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentException"><paramref name="format"/> is invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="width"/> or <paramref name="height"/> are less or equal zero.</exception>
		public FreeImageBitmap(int width, int height, PixelFormat format)
			: base(width, height, format) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified size and type.
		/// Only non standard bitmaps are supported.
		/// </summary>	
		/// <param name="width">The width, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="type">The type of the bitmap.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> is FIT_BITMAP or FIT_UNKNOWN.</exception>
		/// <exception cref="ArgumentException"><paramref name="type"/> is invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="width"/> or <paramref name="height"/> are less or equal zero.</exception>
		public FreeImageBitmap(int width, int height, FREE_IMAGE_TYPE type)
			: base(width, height, type) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified size,
		/// pixel format and pixel data.
		/// </summary>
		/// <param name="width">The width, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="stride">Integer that specifies the byte offset between the beginning
		/// of one scan line and the next. This is usually (but not necessarily)
		/// the number of bytes in the pixel format (for example, 2 for 16 bits per pixel)
		/// multiplied by the width of the bitmap. The value passed to this parameter must
		/// be a multiple of four..</param>
		/// <param name="format">The PixelFormat enumeration for the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="scan0">Pointer to an array of bytes that contains the pixel data.</param>
		/// <remarks>
		/// Although this constructor supports creating images in both formats
		/// <see cref="System.Drawing.Imaging.PixelFormat.Format32bppPArgb"/>
		/// and <see cref="System.Drawing.Imaging.PixelFormat.Format64bppPArgb"/>, bitmaps
		/// created in these formats are treated like any normal 32-bit RGBA and 64-bit RGBA
		/// images respectively. Currently, there is no  support for automatic premultiplying images in
		/// <see cref="FreeImageBitmap"/>.
		/// </remarks>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentException"><paramref name="format"/> is invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="width"/> or <paramref name="height"/> are less or equal zero.</exception>
		public FreeImageBitmap(int width, int height, int stride, PixelFormat format, IntPtr scan0)
			: base(width, height, stride, format, scan0) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified size,
		/// pixel format and pixel data.
		/// </summary>
		/// <param name="width">The width, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="stride">Integer that specifies the byte offset between the beginning
		/// of one scan line and the next. This is usually (but not necessarily)
		/// the number of bytes in the pixel format (for example, 2 for 16 bits per pixel)
		/// multiplied by the width of the bitmap. The value passed to this parameter must
		/// be a multiple of four..</param>
		/// <param name="format">The PixelFormat enumeration for the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="bits">Array of bytes containing the bitmap data.</param>
		/// <remarks>
		/// Although this constructor supports creating images in both formats
		/// <see cref="System.Drawing.Imaging.PixelFormat.Format32bppPArgb"/>
		/// and <see cref="System.Drawing.Imaging.PixelFormat.Format64bppPArgb"/>, bitmaps
		/// created in these formats are treated like any normal 32-bit RGBA and 64-bit RGBA
		/// images respectively. Currently, there is no  support for automatic premultiplying images in
		/// <see cref="FreeImageBitmap"/>.
		/// </remarks>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentException"><paramref name="format"/> is invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="width"/> or <paramref name="height"/> are less or equal zero.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="bits"/> is null</exception>
		public FreeImageBitmap(int width, int height, int stride, PixelFormat format, byte[] bits)
			: base(width, height, stride, format, bits) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified size,
		/// pixel format and pixel data.
		/// </summary>
		/// <param name="width">The width, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="stride">Integer that specifies the byte offset between the beginning
		/// of one scan line and the next. This is usually (but not necessarily)
		/// the number of bytes in the pixel format (for example, 2 for 16 bits per pixel)
		/// multiplied by the width of the bitmap. The value passed to this parameter must
		/// be a multiple of four..</param>
		/// <param name="BPP">The color depth of the new <see cref="FreeImageBitmap"/></param>
		/// <param name="type">The type for the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="scan0">Pointer to an array of bytes that contains the pixel data.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentException"><paramref name="format"/> is invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="width"/> or <paramref name="height"/> are less or equal zero.</exception>
		public FreeImageBitmap(int width, int height, int stride, int bpp, FREE_IMAGE_TYPE type, IntPtr scan0)
			: base(width, height, stride, bpp, type, scan0) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class bases on the specified size,
		/// pixel format and pixel data.
		/// </summary>
		/// <param name="width">The width, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="stride">Integer that specifies the byte offset between the beginning
		/// of one scan line and the next. This is usually (but not necessarily)
		/// the number of bytes in the pixel format (for example, 2 for 16 bits per pixel)
		/// multiplied by the width of the bitmap. The value passed to this parameter must
		/// be a multiple of four..</param>
		/// <param name="BPP">The color depth of the new <see cref="FreeImageBitmap"/></param>
		/// <param name="type">The type for the new <see cref="FreeImageBitmap"/>.</param>
		/// <param name="bits">Array of bytes containing the bitmap data.</param>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="ArgumentException"><paramref name="format"/> is invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="width"/> or <paramref name="height"/> are less or equal zero.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="bits"/> is null</exception>
		public FreeImageBitmap(int width, int height, int stride, int bpp, FREE_IMAGE_TYPE type, byte[] bits)
			: base(width, height, stride, bpp, type, bits) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="FreeImageBitmap"/> class.
		/// </summary>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="SerializationException">The operation failed.</exception>
		public FreeImageBitmap(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		#endregion

		#region Private Methods

		/// <summary>
		/// Tries to replace the wrapped <see cref="FIBITMAP"/> with a new one.
		/// In case the new dib is null or the same as the already
		/// wrapped one, nothing will be changed and the result will
		/// be false.
		/// Otherwise the wrapped <see cref="FIBITMAP"/> will be unloaded and replaced.
		/// </summary>
		/// <param name="newDib">The new dib.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		private bool ReplaceDib(FIBITMAP newDib)
		{
			if ((dib != newDib) && (!newDib.IsNull))
			{
				UnloadDib();
				dib = newDib;
				AddMemoryPressure();
				return true;
			}
			return false;
		}

		#endregion

		#region Mutable Methods

		/// <summary>
		/// This method rotates, flips, or rotates and flips this <see cref="FreeImageBitmapBase"/>.
		/// </summary>
		/// <param name="rotateFlipType">A RotateFlipType member
		/// that specifies the type of rotation and flip to apply to this <see cref="FreeImageBitmapBase"/>.</param>
		public void RotateFlip(RotateFlipType rotateFlipType)
		{
			EnsureNotDisposed();

			FIBITMAP newDib = new FIBITMAP();
			uint bpp = FreeImage.GetBPP(dib);

			switch (rotateFlipType)
			{
				case RotateFlipType.RotateNoneFlipX:

					FreeImage.FlipHorizontal(dib);
					break;

				case RotateFlipType.RotateNoneFlipY:

					FreeImage.FlipVertical(dib);
					break;

				case RotateFlipType.RotateNoneFlipXY:

					FreeImage.FlipHorizontal(dib);
					FreeImage.FlipVertical(dib);
					break;

				case RotateFlipType.Rotate90FlipNone:

					newDib = (bpp == 4u) ? FreeImage.Rotate4bit(dib, 90d) : FreeImage.Rotate(dib, 90d);
					break;

				case RotateFlipType.Rotate90FlipX:

					newDib = (bpp == 4u) ? FreeImage.Rotate4bit(dib, 90d) : FreeImage.Rotate(dib, 90d);
					FreeImage.FlipHorizontal(newDib);
					break;

				case RotateFlipType.Rotate90FlipY:

					newDib = (bpp == 4u) ? FreeImage.Rotate4bit(dib, 90d) : FreeImage.Rotate(dib, 90d);
					FreeImage.FlipVertical(newDib);
					break;

				case RotateFlipType.Rotate90FlipXY:

					newDib = (bpp == 4u) ? FreeImage.Rotate4bit(dib, 90d) : FreeImage.Rotate(dib, 90d);
					FreeImage.FlipHorizontal(newDib);
					FreeImage.FlipVertical(newDib);
					break;

				case RotateFlipType.Rotate180FlipXY:
					newDib = FreeImage.Clone(dib);
					break;
			}
			ReplaceDib(newDib);
		}

		/// <summary>
		/// Selects the frame specified by the index.
		/// </summary>
		/// <param name="frameIndex">The index of the active frame.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="frameIndex"/> is out of range.</exception>
		/// <exception cref="Exception">The operation failed.</exception>
		/// <exception cref="InvalidOperationException">The source of the bitmap is not available.
		/// </exception>
		public void SelectActiveFrame(int frameIndex)
		{
			EnsureNotDisposed();
			if ((frameIndex < 0) || (frameIndex >= frameCount))
			{
				throw new ArgumentOutOfRangeException("frameIndex");
			}

			if (frameIndex != this.frameIndex)
			{
				if (stream == null)
				{
					throw new InvalidOperationException("No source available.");
				}

				FREE_IMAGE_FORMAT format = originalFormat;
				FIMULTIBITMAP mdib = FreeImage.OpenMultiBitmapFromStream(stream, ref format, saveInformation.loadFlags);
				if (mdib.IsNull)
					throw new Exception(ErrorLoadingBitmap);

				try
				{
					if (frameIndex >= FreeImage.GetPageCount(mdib))
					{
						throw new ArgumentOutOfRangeException("frameIndex");
					}

					FIBITMAP newDib = FreeImage.LockPage(mdib, frameIndex);
					if (newDib.IsNull)
					{
						throw new Exception(ErrorLoadingFrame);
					}

					try
					{
						FIBITMAP clone = FreeImage.Clone(newDib);
						if (clone.IsNull)
						{
							throw new Exception(ErrorCreatingBitmap);
						}
						ReplaceDib(clone);
					}
					finally
					{
						if (!newDib.IsNull)
						{
							FreeImage.UnlockPage(mdib, newDib, false);
						}
					}
				}
				finally
				{
					if (!FreeImage.CloseMultiBitmapEx(ref mdib))
					{
						throw new Exception(ErrorUnloadBitmap);
					}
				}

				this.frameIndex = frameIndex;
			}
		}

		/// <summary>
		/// Converts this <see cref="FreeImageBitmapBase"/> into a different color depth.
		/// The parameter <paramref name="BPP"/> specifies color depth, greyscale conversion
		/// and palette reorder.
		/// <para>Adding the <see cref="FREE_IMAGE_COLOR_DEPTH.FICD_FORCE_GREYSCALE"/> flag
		/// will first perform a convesion to greyscale. This can be done with any target
		/// color depth.</para>
		/// <para>Adding the <see cref="FREE_IMAGE_COLOR_DEPTH.FICD_REORDER_PALETTE"/> flag
		/// will allow the algorithm to reorder the palette. This operation will not be performed to
		/// non-greyscale images to prevent data loss by mistake.</para>
		/// </summary>
		/// <param name="BPP">A bitfield containing information about the conversion
		/// to perform.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool ConvertColorDepth(FREE_IMAGE_COLOR_DEPTH bpp)
		{
			EnsureNotDisposed();
			return ReplaceDib(FreeImage.ConvertColorDepth(dib, bpp, false));
		}

		/// <summary>
		/// Converts this <see cref="FreeImageBitmapBase"/> <see cref="FREE_IMAGE_TYPE"/> to
		/// <paramref name="type"/> initializing a new instance.
		/// In case source and destination type are the same, the operation fails.
		/// An error message can be catched using the 'Message' event.
		/// </summary>
		/// <param name="type">Destination type.</param>
		/// <param name="scaleLinear">True to scale linear, else false.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool ConvertType(FREE_IMAGE_TYPE type, bool scaleLinear)
		{
			EnsureNotDisposed();
			return (ImageType == type) ? false : ReplaceDib(FreeImage.ConvertToType(dib, type, scaleLinear));
		}

		/// <summary>
		/// Rescales this <see cref="FreeImageBitmapBase"/> to the specified size using the
		/// specified filter.
		/// </summary>
		/// <param name="newSize">The Size structure that represent the
		/// size of the new <see cref="FreeImageBitmapBase"/>.</param>
		/// <param name="filter">Filter to use for resizing.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Rescale(Size newSize, FREE_IMAGE_FILTER filter)
		{
			return Rescale(newSize.Width, newSize.Height, filter);
		}

		/// <summary>
		/// Rescales this <see cref="FreeImageBitmapBase"/> to the specified size using the
		/// specified filter.
		/// </summary>
		/// <param name="width">Width of the new <see cref="FreeImageBitmapBase"/>.</param>
		/// <param name="height">Height of the new <see cref="FreeImageBitmapBase"/>.</param>
		/// <param name="filter">Filter to use for resizing.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Rescale(int width, int height, FREE_IMAGE_FILTER filter)
		{
			EnsureNotDisposed();
			return ReplaceDib(FreeImage.Rescale(dib, width, height, filter));
		}

		/// <summary>
		/// Quantizes this <see cref="FreeImageBitmapBase"/> from 24 bit to 8bit creating a new
		/// palette with the specified <paramref name="paletteSize"/> using the specified
		/// <paramref name="algorithm"/>.
		/// </summary>
		/// <param name="algorithm">The color reduction algorithm to be used.</param>
		/// <param name="paletteSize">Size of the desired output palette.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Quantize(FREE_IMAGE_QUANTIZE algorithm, int paletteSize)
		{
			return Quantize(algorithm, paletteSize, 0, (RGBQUAD[])null);
		}

		/// <summary>
		/// Quantizes this <see cref="FreeImageBitmapBase"/> from 24 bit to 8bit creating a new
		/// palette with the specified <paramref name="paletteSize"/> using the specified
		/// <paramref name="algorithm"/> and the specified
		/// <paramref name="reservePalette">palette</paramref> up to the
		/// specified <paramref name="paletteSize">length</paramref>.
		/// </summary>
		/// <param name="algorithm">The color reduction algorithm to be used.</param>
		/// <param name="paletteSize">Size of the desired output palette.</param>
		/// <param name="reservePalette">The provided palette.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Quantize(FREE_IMAGE_QUANTIZE algorithm, int paletteSize, Palette reservePalette)
		{
			return Quantize(algorithm, paletteSize, reservePalette.Length, reservePalette.Data);
		}

		/// <summary>
		/// Quantizes this <see cref="FreeImageBitmapBase"/> from 24 bit to 8bit creating a new
		/// palette with the specified <paramref name="paletteSize"/> using the specified
		/// <paramref name="algorithm"/> and the specified
		/// <paramref name="reservePalette">palette</paramref> up to the
		/// specified <paramref name="paletteSize">length</paramref>.
		/// </summary>
		/// <param name="algorithm">The color reduction algorithm to be used.</param>
		/// <param name="paletteSize">Size of the desired output palette.</param>
		/// <param name="reserveSize">Size of the provided palette of ReservePalette.</param>
		/// <param name="reservePalette">The provided palette.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Quantize(FREE_IMAGE_QUANTIZE algorithm, int paletteSize, int reserveSize, Palette reservePalette)
		{
			return Quantize(algorithm, paletteSize, reserveSize, reservePalette.Data);
		}

		/// <summary>
		/// Quantizes this <see cref="FreeImageBitmapBase"/> from 24 bit to 8bit creating a new
		/// palette with the specified <paramref name="paletteSize"/> using the specified
		/// <paramref name="algorithm"/> and the specified
		/// <paramref name="reservePalette">palette</paramref> up to the
		/// specified <paramref name="paletteSize">length</paramref>.
		/// </summary>
		/// <param name="algorithm">The color reduction algorithm to be used.</param>
		/// <param name="paletteSize">Size of the desired output palette.</param>
		/// <param name="reserveSize">Size of the provided palette of ReservePalette.</param>
		/// <param name="reservePalette">The provided palette.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Quantize(FREE_IMAGE_QUANTIZE algorithm, int paletteSize, int reserveSize, RGBQUAD[] reservePalette)
		{
			EnsureNotDisposed();
			return ReplaceDib(FreeImage.ColorQuantizeEx(dib, algorithm, paletteSize, reserveSize, reservePalette));
		}

		/// <summary>
		/// Enlarges or shrinks this <see cref="FreeImageBitmapBase"/> selectively per side and fills
		/// newly added areas with the specified background color.
		/// See <see cref="FreeImage.EnlargeCanvas&lt;T&gt;"/> for further details.
		/// </summary>
		/// <typeparam name="T">The type of the specified color.</typeparam>
		/// <param name="left">The number of pixels, the image should be enlarged on its left side.
		/// Negative values shrink the image on its left side.</param>
		/// <param name="top">The number of pixels, the image should be enlarged on its top side.
		/// Negative values shrink the image on its top side.</param>
		/// <param name="right">The number of pixels, the image should be enlarged on its right side.
		/// Negative values shrink the image on its right side.</param>
		/// <param name="bottom">The number of pixels, the image should be enlarged on its bottom side.
		/// Negative values shrink the image on its bottom side.</param>
		/// <param name="color">The color, the enlarged sides of the image should be filled with.</param>
		/// <returns><c>true</c> on success, <c>false</c> on failure.</returns>
		public bool EnlargeCanvas<T>(int left, int top, int right, int bottom, T? color) where T : struct
		{
			return EnlargeCanvas(left, top, right, bottom, color, FREE_IMAGE_COLOR_OPTIONS.DEFAULT);
		}

		/// <summary>
		/// Enlarges or shrinks this <see cref="FreeImageBitmapBase"/> selectively per side and fills
		/// newly added areas with the specified background color.
		/// See <see cref="FreeImage.EnlargeCanvas&lt;T&gt;"/> for further details.
		/// </summary>
		/// <typeparam name="T">The type of the specified color.</typeparam>
		/// <param name="left">The number of pixels, the image should be enlarged on its left side.
		/// Negative values shrink the image on its left side.</param>
		/// <param name="top">The number of pixels, the image should be enlarged on its top side.
		/// Negative values shrink the image on its top side.</param>
		/// <param name="right">The number of pixels, the image should be enlarged on its right side.
		/// Negative values shrink the image on its right side.</param>
		/// <param name="bottom">The number of pixels, the image should be enlarged on its bottom side.
		/// Negative values shrink the image on its bottom side.</param>
		/// <param name="color">The color, the enlarged sides of the image should be filled with.</param>
		/// <param name="options">Options that affect the color search process for palletized images.</param>
		/// <returns><c>true</c> on success, <c>false</c> on failure.</returns>
		public bool EnlargeCanvas<T>(int left, int top, int right, int bottom,
			T? color, FREE_IMAGE_COLOR_OPTIONS options) where T : struct
		{
			EnsureNotDisposed();
			return ReplaceDib(FreeImage.EnlargeCanvas(dib, left, top, right, bottom, color, options));
		}

		/// <summary>
		/// Converts a High Dynamic Range image to a 24-bit RGB image using a global
		/// operator based on logarithmic compression of luminance values, imitating
		/// the human response to light.
		/// </summary>
		/// <param name="gamma">A gamma correction that is applied after the tone mapping.
		/// A value of 1 means no correction.</param>
		/// <param name="exposure">Scale factor allowing to adjust the brightness of the output image.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool TmoDrago03(double gamma, double exposure)
		{
			EnsureNotDisposed();
			return ReplaceDib(FreeImage.TmoDrago03(dib, gamma, exposure));
		}

		/// <summary>
		/// Converts a High Dynamic Range image to a 24-bit RGB image using a global operator inspired
		/// by photoreceptor physiology of the human visual system.
		/// </summary>
		/// <param name="intensity">Controls the overall image intensity in the range [-8, 8].</param>
		/// <param name="contrast">Controls the overall image contrast in the range [0.3, 1.0[.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool TmoReinhard05(double intensity, double contrast)
		{
			EnsureNotDisposed();
			return ReplaceDib(FreeImage.TmoReinhard05(dib, intensity, contrast));
		}

		/// <summary>
		/// Apply the Gradient Domain High Dynamic Range Compression to a RGBF image and convert to 24-bit RGB.
		/// </summary>
		/// <param name="color_saturation">Color saturation (s parameter in the paper) in [0.4..0.6]</param>
		/// <param name="attenuation">Atenuation factor (beta parameter in the paper) in [0.8..0.9]</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool TmoFattal02(double color_saturation, double attenuation)
		{
			EnsureNotDisposed();
			return ReplaceDib(FreeImage.TmoFattal02(dib, color_saturation, attenuation));
		}

		/// <summary>
		/// This method rotates a 1-, 4-, 8-bit greyscale or a 24-, 32-bit color image by means of 3 shears.
		/// For 1- and 4-bit images, rotation is limited to angles whose value is an integer
		/// multiple of 90.
		/// </summary>
		/// <param name="angle">The angle of rotation.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Rotate(double angle)
		{
			EnsureNotDisposed();
			bool result = false;
			if (ColorDepth == 4)
			{
				result = ReplaceDib(FreeImage.Rotate4bit(dib, angle));
			}
			else
			{
				result = ReplaceDib(FreeImage.Rotate(dib, angle));
			}
			return result;
		}

		/// <summary>
		/// This method rotates a 1-, 4-, 8-bit greyscale or a 24-, 32-bit color image by means of 3 shears.
		/// For 1- and 4-bit images, rotation is limited to angles whose value is an integer
		/// multiple of 90.
		/// </summary>
		/// <typeparam name="T">The type of the color to use as background.</typeparam>
		/// <param name="angle">The angle of rotation.</param>
		/// <param name="backgroundColor">The color used used to fill the bitmap's background.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Rotate<T>(double angle, T? backgroundColor) where T : struct
		{
			EnsureNotDisposed();
			bool result = false;
			if (ColorDepth == 4)
			{
				result = ReplaceDib(FreeImage.Rotate4bit(dib, angle));
			}
			else
			{
				result = ReplaceDib(FreeImage.Rotate(dib, angle, backgroundColor));
			}
			return result;
		}

		/// <summary>
		/// This method performs a rotation and / or translation of an 8-bit greyscale,
		/// 24- or 32-bit image, using a 3rd order (cubic) B-Spline.
		/// </summary>
		/// <param name="angle">The angle of rotation.</param>
		/// <param name="xShift">Horizontal image translation.</param>
		/// <param name="yShift">Vertical image translation.</param>
		/// <param name="xOrigin">Rotation center x-coordinate.</param>
		/// <param name="yOrigin">Rotation center y-coordinate.</param>
		/// <param name="useMask">When true the irrelevant part of the image is set to a black color,
		/// otherwise, a mirroring technique is used to fill irrelevant pixels.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Rotate(double angle, double xShift, double yShift,
			double xOrigin, double yOrigin, bool useMask)
		{
			EnsureNotDisposed();
			return ReplaceDib(FreeImage.RotateEx(dib, angle, xShift, yShift, xOrigin, yOrigin, useMask));
		}

		/// <summary>
		/// This method composite a transparent foreground image against a single background color or
		/// against a background image.
		/// In case <paramref name="useBitmapBackground"/> is false and <paramref name="applicationBackground"/>
		/// and <paramref name="bitmapBackGround"/>
		/// are null, a checkerboard will be used as background.
		/// </summary>
		/// <param name="useBitmapBackground">When true the background of this instance is used
		/// if it contains one.</param>
		/// <param name="applicationBackground">Backgroundcolor used in case <paramref name="useBitmapBackground"/> is false
		/// and <paramref name="applicationBackground"/> is not null.</param>
		/// <param name="bitmapBackGround">Background used in case <paramref name="useBitmapBackground"/>
		/// is false and <paramref name="applicationBackground"/> is a null reference.</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool Composite(bool useBitmapBackground, Color? applicationBackground, FreeImageBitmap bitmapBackGround)
		{
			EnsureNotDisposed();
			bitmapBackGround.EnsureNotDisposed();

			RGBQUAD? rgb = applicationBackground;
			return ReplaceDib(
				FreeImage.Composite(
					dib,
					useBitmapBackground,
					rgb.HasValue ? new RGBQUAD[] { rgb.Value } : null,
					bitmapBackGround.dib));
		}

		/// <summary>
		/// Solves a Poisson equation, remap result pixels to [0..1] and returns the solution.
		/// </summary>
		/// <param name="ncycle">Number of cycles in the multigrid algorithm (usually 2 or 3)</param>
		/// <returns>Returns true on success, false on failure.</returns>
		public bool MultigridPoissonSolver(int ncycle)
		{
			EnsureNotDisposed();
			return ReplaceDib(FreeImage.MultigridPoissonSolver(dib, ncycle));
		}

		#endregion
	}
}