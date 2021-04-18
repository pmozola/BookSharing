using System;
using System.Collections.Generic;

namespace BookSharing.Infrastructure.BookApi
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class IndustryIdentifier
    {
        public string type { get; set; }
        public string identifier { get; set; }
    }

    public class ReadingModes
    {
        public bool text { get; set; }
        public bool image { get; set; }
    }

    public class PanelizationSummary
    {
        public bool containsEpubBubbles { get; set; }
        public bool containsImageBubbles { get; set; }
    }

    public class ImageLinks
    {
        public string smallThumbnail { get; set; }
        public string thumbnail { get; set; }
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
        public string extraLarge { get; set; }
    }

    public class VolumeInfo
    {
        public string title { get; set; }
        public List<string> authors { get; set; }
        public string publisher { get; set; }
        public string publishedDate { get; set; }
        public string description { get; set; }
        public List<IndustryIdentifier> industryIdentifiers { get; set; }
        public ReadingModes readingModes { get; set; }
        public int pageCount { get; set; }
        public int printedPageCount { get; set; }
        public string printType { get; set; }
        public List<string> categories { get; set; }
        public string maturityRating { get; set; }
        public bool allowAnonLogging { get; set; }
        public string contentVersion { get; set; }
        public PanelizationSummary panelizationSummary { get; set; }
        public ImageLinks imageLinks { get; set; }
        public string language { get; set; }
        public string previewLink { get; set; }
        public string infoLink { get; set; }
        public string canonicalVolumeLink { get; set; }
    }

    public class Layer
    {
        public string layerId { get; set; }
        public string volumeAnnotationsVersion { get; set; }
    }

    public class LayerInfo
    {
        public List<Layer> layers { get; set; }
    }

    public class ListPrice
    {
        public double amount { get; set; }
        public string currencyCode { get; set; }
        public int amountInMicros { get; set; }
    }

    public class RetailPrice
    {
        public double amount { get; set; }
        public string currencyCode { get; set; }
        public int amountInMicros { get; set; }
    }

    public class Offer
    {
        public int finskyOfferType { get; set; }
        public ListPrice listPrice { get; set; }
        public RetailPrice retailPrice { get; set; }
    }

    public class SaleInfo
    {
        public string country { get; set; }
        public string saleability { get; set; }
        public bool isEbook { get; set; }
        public ListPrice listPrice { get; set; }
        public RetailPrice retailPrice { get; set; }
        public string buyLink { get; set; }
        public List<Offer> offers { get; set; }
    }

    public class Epub
    {
        public bool isAvailable { get; set; }
        public string acsTokenLink { get; set; }
    }

    public class Pdf
    {
        public bool isAvailable { get; set; }
        public string acsTokenLink { get; set; }
    }

    public class AccessInfo
    {
        public string country { get; set; }
        public string viewability { get; set; }
        public bool embeddable { get; set; }
        public bool publicDomain { get; set; }
        public string textToSpeechPermission { get; set; }
        public Epub epub { get; set; }
        public Pdf pdf { get; set; }
        public string webReaderLink { get; set; }
        public string accessViewStatus { get; set; }
        public bool quoteSharingAllowed { get; set; }
    }

    public class BookOpenApiResource
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }
        public VolumeInfo volumeInfo { get; set; }
        public LayerInfo layerInfo { get; set; }
        public SaleInfo saleInfo { get; set; }
        public AccessInfo accessInfo { get; set; }
    }



    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    //public class LastModified
    //{
    //    public string type { get; set; }
    //    public DateTime value { get; set; }
    //}

    //public class Author
    //{
    //    public string key { get; set; }
    //}

    //public class Language
    //{
    //    public string key { get; set; }
    //}

    //public class Classifications
    //{
    //}

    //public class Identifiers
    //{
    //    public List<string> goodreads { get; set; }
    //    public List<string> librarything { get; set; }
    //}

    //public class Created
    //{
    //    public string type { get; set; }
    //    public DateTime value { get; set; }
    //}

    //public class Work
    //{
    //    public string key { get; set; }
    //}

    //public class Type
    //{
    //    public string key { get; set; }
    //}

    //public class FirstSentence
    //{
    //    public string type { get; set; }
    //    public string value { get; set; }
    //}

    //public class BookOpenApiResource
    //{
    //    public List<string> publishers { get; set; }
    //    public int number_of_pages { get; set; }
    //    public List<string> isbn_10 { get; set; }
    //    public List<int> covers { get; set; }
    //    public LastModified last_modified { get; set; }
    //    public int latest_revision { get; set; }
    //    public string key { get; set; }
    //    public List<Author> authors { get; set; }
    //    public string ocaid { get; set; }
    //    public List<string> contributions { get; set; }
    //    public List<Language> languages { get; set; }
    //    public Classifications classifications { get; set; }
    //    public List<string> source_records { get; set; }
    //    public string title { get; set; }
    //    public Identifiers identifiers { get; set; }
    //    public Created created { get; set; }
    //    public List<string> isbn_13 { get; set; }
    //    public List<string> local_id { get; set; }
    //    public string publish_date { get; set; }
    //    public List<Work> works { get; set; }
    //    public Type type { get; set; }
    //    public FirstSentence first_sentence { get; set; }
    //    public int revision { get; set; }
    //}
}
