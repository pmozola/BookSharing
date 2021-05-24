using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSharing.Infrastructure.BookApi.OpenLibrary
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Doc
    {
        public string key { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string title_suggest { get; set; }
        public bool has_fulltext { get; set; }
        public int edition_count { get; set; }
        public int first_publish_year { get; set; }
        public int last_modified_i { get; set; }
        public int ebook_count_i { get; set; }
        public bool public_scan_b { get; set; }
        public string ia_collection_s { get; set; }
        public string lending_edition_s { get; set; }
        public string lending_identifier_s { get; set; }
        public string printdisabled_s { get; set; }
        public string cover_edition_key { get; set; }
        public int cover_i { get; set; }
        public List<int> publish_year { get; set; }
        public List<string> author_name { get; set; }
        public List<string> id_amazon { get; set; }
        public List<string> seed { get; set; }
        public List<string> author_alternative_name { get; set; }
        public List<string> subject { get; set; }
        public List<long> isbn { get; set; }
        public List<string> ia_loaded_id { get; set; }
        public List<string> edition_key { get; set; }
        public List<string> language { get; set; }
        public List<string> id_librarything { get; set; }
        public List<string> lcc { get; set; }
        public List<string> lccn { get; set; }
        public List<string> id_goodreads { get; set; }
        public List<string> publish_place { get; set; }
        public List<string> contributor { get; set; }
        public List<string> id_google { get; set; }
        public List<string> ia { get; set; }
        public List<string> text { get; set; }
        public List<string> place { get; set; }
        public List<string> ddc { get; set; }
        public List<string> author_key { get; set; }
        public List<string> id_overdrive { get; set; }
        public List<string> id_depósito_legal { get; set; }
        public List<string> id_alibris_id { get; set; }
        public List<string> id_canadian_national_library_archive { get; set; }
        public List<string> ia_box_id { get; set; }
        public List<string> person { get; set; }
        public List<string> id_wikidata { get; set; }
        public List<string> oclc { get; set; }
        public List<string> publisher { get; set; }
        public List<string> time { get; set; }
        public List<string> publish_date { get; set; }
        public List<string> id_paperback_swap { get; set; }
        public string subtitle { get; set; }
        public List<string> first_sentence { get; set; }
        public List<string> id_amazon_de_asin { get; set; }
        public List<string> id_amazon_it_asin { get; set; }
        public List<string> id_nla { get; set; }
        public List<string> id_british_national_bibliography { get; set; }
        public List<string> id_amazon_co_uk_asin { get; set; }
        public List<string> id_amazon_ca_asin { get; set; }
        public List<string> id_bcid { get; set; }
    }

    public class OpenLibraryBookResponseByTitle
    {
        public int numFound { get; set; }
        public int start { get; set; }
        public Doc docs { get; set; }
        public int num_found { get; set; }
    }
}
