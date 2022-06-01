#if !Mono
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace FreeImageAPI.Metadata
{
	/// <summary>
	/// Windows Imaging Component Metadata Handler. See <see cref="BitmapMetadata"/>.
	/// </summary>
	/// <remarks>
	/// <para>Modified metadata isn't saved to the <see cref="FreeImageBitmapBase"/> until the <see cref="Save"/>-Method is called.</para>
	/// <para>This class is not supported on Mono.</para>
	/// </remarks>
	/// <seealso cref="BitmapMetadata"/>
	/// <seealso href="https://msdn.microsoft.com/en-us/library/ee719796.aspx">Metadata Query Language Overview</seealso>
	/// <seealso href="https://msdn.microsoft.com/en-us/library/ee719904.aspx">Native Image Format Metadata Queries</seealso>
	/// <seealso href="https://msdn.microsoft.com/en-us/library/ee872003(v=vs.85).aspx">Photo Metadata Policies</seealso>
	/// <include file='Documentation/WICMetadataHandler.xml' path='WICMetadataHandler/Examples/*'/>
	public class WICMetadataHandler
	{
		FreeImageBitmapBase _image;
		BitmapMetadata _bMetadata;
		BitmapFrame _frame;

		public WICMetadataHandler(FreeImageBitmapBase image)
		{
			this._image = image;
			using (MemoryStream buffer = new MemoryStream(1024))
			{
				using (FreeImageBitmapBase temp = new FreeImageBitmapBase(1, 1))
				{
					temp.CloneMetadataFrom(image);
					temp.Save(buffer, FREE_IMAGE_FORMAT.FIF_JPEG);
				}
				buffer.Seek(0, SeekOrigin.Begin);
				JpegBitmapDecoder jpgDecoder = new JpegBitmapDecoder(buffer, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
				this._frame = jpgDecoder.Frames[0];
				this._bMetadata = this._frame.Metadata.Clone() as BitmapMetadata;
			}
		}

		/// <summary>
		/// Saves the metadata to the underlying <see cref="FreeImageBitmapBase"/>. Modified metadata
		/// is not saved until this method is called.
		/// </summary>
		public void Save()
		{
			using (MemoryStream buffer = new MemoryStream(1024))
			{
				BitmapFrame frame = BitmapFrame.Create(this._frame, this._frame.Thumbnail, this._bMetadata, this._frame.ColorContexts);
				JpegBitmapEncoder jpgEncoder = new JpegBitmapEncoder();
				jpgEncoder.Frames.Add(frame);
				jpgEncoder.Save(buffer);
				buffer.Seek(0, SeekOrigin.Begin);
				using (FreeImageBitmapBase temp = new FreeImageBitmapBase(buffer, FREE_IMAGE_FORMAT.FIF_JPEG))
				{
					this._image.CloneMetadataFrom(temp);
				}
			}
		}

		/// <inheritdoc cref="BitmapMetadata.ContainsQuery"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee719796.aspx">Metadata Query Language Overview</seealso>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee719904.aspx">Native Image Format Metadata Queries</seealso>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee872003.aspx">Photo Metadata Policies</seealso>
		/// <include file='Documentation/WICMetadataHandler.xml' path='WICMetadataHandler/Examples/*'/>
		public bool ContainsQuery(string query)
		{
			return this._bMetadata.ContainsQuery(query);
		}

		/// <inheritdoc cref="BitmapMetadata.GetQuery"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee719796.aspx">Metadata Query Language Overview</seealso>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee719904.aspx">Native Image Format Metadata Queries</seealso>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee872003.aspx">Photo Metadata Policies</seealso>
		/// <include file='Documentation/WICMetadataHandler.xml' path='WICMetadataHandler/Examples/*'/>
		public Object GetQuery(string query)
		{
			return this._bMetadata.GetQuery(query);
		}

		/// <inheritdoc cref="BitmapMetadata.RemoveQuery"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee719796.aspx">Metadata Query Language Overview</seealso>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee719904.aspx">Native Image Format Metadata Queries</seealso>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee872003.aspx">Photo Metadata Policies</seealso>
		/// <include file='Documentation/WICMetadataHandler.xml' path='WICMetadataHandler/Examples/*'/>
		public void RemoveQuery(string query)
		{
			this._bMetadata.RemoveQuery(query);
		}

		/// <inheritdoc cref="BitmapMetadata.SetQuery"/>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee719796.aspx">Metadata Query Language Overview</seealso>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee719904.aspx">Native Image Format Metadata Queries</seealso>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/ee872003.aspx">Photo Metadata Policies</seealso>
		/// <include file='Documentation/WICMetadataHandler.xml' path='WICMetadataHandler/Examples/*'/>
		public void SetQuery(string query, Object value)
		{
			this._bMetadata.SetQuery(query, value);
		}

		/// <summary>
		/// Set multiple queries.
		/// </summary>
		/// <param name="queries"></param>
		/// <seealso cref="SetQuery"/>
		public void SetQueries(Dictionary<string,object> queries)
		{
			foreach (KeyValuePair<string, object> item in queries)
				this.SetQuery(item.Key, item.Value);
		}

		/// <summary>
		/// Get all metadata queries.
		/// </summary>
		public Dictionary<string,object> GetAllQueries()
		{
			Dictionary<string, object> RetVal = new Dictionary<string, object>(64);
			AddQueries(this._bMetadata, ref RetVal);
			return RetVal;
		}

		static void AddQueries(BitmapMetadata bMeta, ref Dictionary<string,object> dict)
		{
			List<BitmapMetadata> metas = new List<BitmapMetadata>();
			foreach (string query in bMeta)
			{
				object obj = bMeta.GetQuery(query);
				if (obj is BitmapMetadata)
					metas.Add((BitmapMetadata)obj);
				else if (obj != null)
					dict.Add(bMeta.Location + query, obj);
			}
			foreach (BitmapMetadata m in metas)
				AddQueries(m, ref dict);
		}

		#region Properties

		/// <summary>
		/// Gets or sets a value that identifies the name of the application that was used to construct or alter an image file.
		/// </summary>
		public string ApplicationName
		{
			get { return this._bMetadata.ApplicationName; }
			set { this._bMetadata.ApplicationName = value; }
		}

		/// <summary>
		/// Gets or sets a value that represents the author of an image.
		/// </summary>
		public ReadOnlyCollection<string> Author
		{
			get { return this._bMetadata.Author; }
			set { this._bMetadata.Author = value; }
		}

		/// <summary>
		/// Gets or sets a value that identifies the camera manufacturer that is associated with an image.
		/// </summary>
		public string CameraManufacturer
		{
			get { return this._bMetadata.CameraManufacturer; }
			set { this._bMetadata.CameraManufacturer = value; }
		}

		/// <summary>
		/// Gets or sets a value that identifies the camera model that was used to capture the image.
		/// </summary>
		public string  CameraModel
		{
			get { return this._bMetadata.CameraModel; }
			set { this._bMetadata.CameraModel = value; }
		}

		/// <summary>
		/// Gets or sets a value that represents a comment that is associated with the image file. 
		/// </summary>
		public string Comment
		{
			get { return this._bMetadata.Comment; }
			set { this._bMetadata.Comment = value; }
		}

		/// <summary>
		/// Gets or sets a value that indicates copyright information that is associated with the image file.
		/// </summary>
		public string Copyright
		{
			get { return this._bMetadata.Copyright; }
			set { this._bMetadata.Copyright = value; }
		}

		/// <summary>
		/// Gets or sets a value that indicates the date that the image was taken.
		/// </summary>
		public DateTime? DateTaken
		{
			get
			{
				DateTime RetVal;
				if (DateTime.TryParse(this._bMetadata.DateTaken, out RetVal))
				{
					return new DateTime(RetVal.Ticks, DateTimeKind.Local);
				}
				return null;
			}
			set
			{
				if (value.HasValue)
					this._bMetadata.DateTaken = value.Value.ToString("o");
				else
					this._bMetadata.DateTaken = null;
			}
		}

		/// <summary>
		/// Gets or sets a collection of keywords that describe the bitmap image.
		/// </summary>
		public ReadOnlyCollection<string> Keywords
		{
			get { return this._bMetadata.Keywords; }
			set { this._bMetadata.Keywords = value; }
		}

		/// <summary>
		/// Gets or sets a value that identifies the image rating.
		/// </summary>
		/// <value>The rating value between 0 and 5.</value>
		public int Rating
		{
			get { return this._bMetadata.Rating; }
			set { this._bMetadata.Rating = value; }
		}

		/// <summary>
		/// Gets or sets a value that indicates the subject matter of the bitmap image.
		/// </summary>
		public string Subject
		{
			get { return this._bMetadata.Subject; }
			set { this._bMetadata.Subject = value; }
		}

		/// <summary>
		/// Gets or sets a value that identifies the title of an image file.
		/// </summary>
		public string Title
		{
			get { return this._bMetadata.Title; }
			set { this._bMetadata.Title = value; }
		}

		#endregion
	}
}
#endif