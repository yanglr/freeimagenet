namespace FreeImageAPI
{
	[System.Flags]
	public enum FREE_IMAGE_RESCALE_FLAGS : int
	{
		/// <summary>
		/// default options; none of the following other options apply
		/// </summary>
		FI_RESCALE_DEFAULT			= 0x00,
		/// <summary>
		/// for non-transparent greyscale images, convert to 24-bit if src bitdepth <= 8 (default is a 8-bit greyscale image). 
		/// </summary>
		FI_RESCALE_TRUE_COLOR		= 0x01,
		/// <summary>
		/// do not copy metadata to the rescaled image
		/// </summary>
		FI_RESCALE_OMIT_METADATA	= 0x02
	}
}
