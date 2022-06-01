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
// $Revision: 1.1 $
// $Date: 2009/09/15 11:44:24 $
// $Id: FREE_IMAGE_COLOR_OPTIONS.cs,v 1.1 2009/09/15 11:44:24 cklein05 Exp $
// ==========================================================

namespace FreeImageAPI
{
	/// <summary>
	/// Constants used in color filling routines.
	/// </summary>
	public enum FREE_IMAGE_COLOR_OPTIONS
	{
		/// <summary>
		/// Default value, same as <see cref="IS_RGB_COLOR"/>.
		/// </summary>
		DEFAULT = IS_RGB_COLOR,
		/// <summary>
		/// <see cref="RGBQUAD"/> color is RGB color (contains no valid alpha channel).
		/// </summary>
		IS_RGB_COLOR = 0x0,
		/// <summary>
		/// <see cref="RGBQUAD"/> color is RGBA color (contains a valid alpha channel).
		/// </summary>
		IS_RGBA_COLOR = 0x1,
		/// <summary>
		/// Lookup equal RGB color from palette.
		/// </summary>
		FIND_EQUAL_COLOR = 0x2,
		/// <summary>
		/// <see cref="RGBQUAD.rgbReserved"/> contains the palette index to be used.
		/// </summary>
		ALPHA_IS_INDEX = 0x4,
		/// <summary>
		/// No color lookup is performed
		/// </summary>
		PALETTE_SEARCH_MASK = (FIND_EQUAL_COLOR | ALPHA_IS_INDEX)
	}
}