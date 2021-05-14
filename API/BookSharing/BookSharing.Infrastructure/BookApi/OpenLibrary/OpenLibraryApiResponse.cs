using System;
using System.Collections.Generic;

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA1050 // Declare types in namespaces
public class TableOfContent

{

    public int level { get; set; }

    public string label { get; set; }
    public string title { get; set; }
    public string pagenum { get; set; }
}

public class Contributor
{
    public string role { get; set; }
    public string name { get; set; }
}

public class Language
{
    public string key { get; set; }
}

public class Type
{
    public string key { get; set; }
}

public class Author
{
    public string key { get; set; }
    public string name { get; set; }
}

public class Classifications
{
}

public class Identifiers
{
    public List<string> amazon { get; set; }
    public List<string> google { get; set; }
    public List<string> librarything { get; set; }
    public List<string> goodreads { get; set; }
}

public class Work
{
    public string key { get; set; }
}

public class Created
{
    public string type { get; set; }
    public DateTime value { get; set; }
}

public class LastModified
{
    public string type { get; set; }
    public DateTime value { get; set; }
}

public class Details
{
    public int number_of_pages { get; set; }
    public List<TableOfContent> table_of_contents { get; set; }
    public List<Contributor> contributors { get; set; }
    public List<string> isbn_10 { get; set; }
    public List<int> covers { get; set; }
    public List<string> lc_classifications { get; set; }
    public string ocaid { get; set; }
    public string weight { get; set; }
    public List<string> source_records { get; set; }
    public string title { get; set; }
    public List<Language> languages { get; set; }
    public List<string> subjects { get; set; }
    public string publish_country { get; set; }
    public string by_statement { get; set; }
    public List<string> oclc_numbers { get; set; }
    public Type type { get; set; }
    public string physical_dimensions { get; set; }
    public List<string> publishers { get; set; }
    public string description { get; set; }
    public string physical_format { get; set; }
    public string key { get; set; }
    public List<Author> authors { get; set; }
    public List<string> publish_places { get; set; }
    public string pagination { get; set; }
    public Classifications classifications { get; set; }
    public List<string> lccn { get; set; }
    public Identifiers identifiers { get; set; }
    public List<string> isbn_13 { get; set; }
    public List<string> dewey_decimal_class { get; set; }
    public List<string> local_id { get; set; }
    public string publish_date { get; set; }
    public List<Work> works { get; set; }
    public int latest_revision { get; set; }
    public int revision { get; set; }
    public Created created { get; set; }
    public LastModified last_modified { get; set; }
}


public class OpenLibraryBookResponse
{
    public string bib_key { get; set; }
    public string info_url { get; set; }
    public string preview { get; set; }
    public string preview_url { get; set; }
    public string thumbnail_url { get; set; }
    public Details details { get; set; }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CA1050 // Declare types in namespaces