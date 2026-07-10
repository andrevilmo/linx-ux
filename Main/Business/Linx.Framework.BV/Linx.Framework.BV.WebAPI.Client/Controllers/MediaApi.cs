using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Http;
using Linx.Framework.BV.Multimidia;

namespace Linx.Framework.BV.WebAPI.Client.Controllers
{

	public partial class MediaApi : LinxClientApiController
	{

		public MediaApi(string serviceBusAddress) : base(serviceBusAddress) {  }

		public List<MediaElement> GetPendingMedias(byte documentoType=0)
		{
		      List<MediaElement> result = default(List<MediaElement>);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetPendingMedias?documentoType=" + (documentoType.ToString(System.Globalization.CultureInfo.InvariantCulture)) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<List<MediaElement>>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public byte[] GetMediaContent(System.Guid id)
		{
		      byte[] result = default(byte[]);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetMediaContent?id=" + (id.ToString()) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<byte[]>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public string UpdateMedias(System.Collections.Generic.List<Linx.Framework.BV.Multimidia.MediaElement> medias)
		{
		      string result = default(string);
		      HttpResponseMessage response = _client.PostAsJsonAsync("MediaApi/UpdateMedias", medias).Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<string>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public List<string> GetMediaUrlById(string tableName, int pkValue)
		{
		      List<string> result = default(List<string>);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetMediaUrlById?tableName=" + (tableName) + "&pkValue=" + (pkValue.ToString(System.Globalization.CultureInfo.InvariantCulture)) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<List<string>>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public List<string> GetMediaUrlByUid(string tableName, System.Guid pkValue)
		{
		      List<string> result = default(List<string>);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetMediaUrlByUid?tableName=" + (tableName) + "&pkValue=" + (pkValue.ToString()) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<List<string>>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public List<byte[]> GetMediaContentById(string tableName, int pkValue)
		{
		      List<byte[]> result = default(List<byte[]>);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetMediaContentById?tableName=" + (tableName) + "&pkValue=" + (pkValue.ToString(System.Globalization.CultureInfo.InvariantCulture)) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<List<byte[]>>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public List<byte[]> GetMediaContentByUid(string tableName, System.Guid pkValue)
		{
		      List<byte[]> result = default(List<byte[]>);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetMediaContentByUid?tableName=" + (tableName) + "&pkValue=" + (pkValue.ToString()) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<List<byte[]>>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public List<byte[]> GetMediaThumbnailById(string tableName, int pkValue)
		{
		      List<byte[]> result = default(List<byte[]>);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetMediaThumbnailById?tableName=" + (tableName) + "&pkValue=" + (pkValue.ToString(System.Globalization.CultureInfo.InvariantCulture)) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<List<byte[]>>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public List<byte[]> GetMediaThumbnailByUid(string tableName, System.Guid pkValue)
		{
		      List<byte[]> result = default(List<byte[]>);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetMediaThumbnailByUid?tableName=" + (tableName) + "&pkValue=" + (pkValue.ToString()) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<List<byte[]>>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public byte[] GetMediaThumbnail(System.Guid id)
		{
		      byte[] result = default(byte[]);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetMediaThumbnail?id=" + (id.ToString()) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<byte[]>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public string UpdateMedia(Linx.Framework.BV.Multimidia.MediaElement media)
		{
		      string result = default(string);
		      HttpResponseMessage response = _client.PostAsJsonAsync("MediaApi/UpdateMedia", media).Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<string>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public List<MediaConfigLength> GetMediaConfigLength()
		{
		      List<MediaConfigLength> result = default(List<MediaConfigLength>);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetMediaConfigLength").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<List<MediaConfigLength>>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public List<string> GetMediaUrlThumbById(string tableName, int pkValue, int usabilityId)
		{
		      List<string> result = default(List<string>);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetMediaUrlThumbById?tableName=" + (tableName) + "&pkValue=" + (pkValue.ToString(System.Globalization.CultureInfo.InvariantCulture)) + "&usabilityId=" + (usabilityId.ToString(System.Globalization.CultureInfo.InvariantCulture)) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<List<string>>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public List<string> GetMediaUrlThumbByUid(string tableName, System.Guid pkValue, int usabilityId)
		{
		      List<string> result = default(List<string>);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetMediaUrlThumbByUid?tableName=" + (tableName) + "&pkValue=" + (pkValue.ToString()) + "&usabilityId=" + (usabilityId.ToString(System.Globalization.CultureInfo.InvariantCulture)) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<List<string>>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}

		public List<MediaElement> GetEffectiveMedias(byte documentoType=0)
		{
		      List<MediaElement> result = default(List<MediaElement>);
		      HttpResponseMessage response = _client.GetAsync("MediaApi/GetEffectiveMedias?documentoType=" + (documentoType.ToString(System.Globalization.CultureInfo.InvariantCulture)) + "").Result;
		      if (response.IsSuccessStatusCode)
		          result = response.Content.ReadAsAsync<List<MediaElement>>().Result;
		      else
		      {
		          var responseContent = response.Content.ReadAsStringAsync();
		          responseContent.Wait();
		          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));
		      }
		      return result;
		}
	}
}
