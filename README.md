# SharpContentScraper
This is a web scarper library written in C#. This is a scarping tool to enable C# programmers to parse web page in a much simpler way.

Example:

testing123.com
```html
<html>
<head>
<title>example </title>
<body>
  <article class="article-style">
      <a href="http://testing123.com/article1">article 1</a>
      <div class="article-content">article content</div>
      <div class="article-time"> 10/31/2017 <div>
  </article>
  <article class="article-style">
      <a href="http://testing123.com/article2">article 2</a>
      <div class="article-content">article content</div>
      <div class="article-time"> 10/31/2017 <div>
  </article>
  <article class="article-style">
      <a href="http://testing123.com/article3">article 3</a>
      <div class="article-content">article content</div>
      <div class="article-time"> 10/31/2017 <div>
  </article>
        
</body>
</head>
</html>
```

You can define a C# model like this:
```C#
public class Article{
  public string Title {get;set;}
  public string Url {get;set;}
  public string Content {get;set;}
  public DateTime CreationTime {get;set;}
}
```

The C# code to map the html code to a collection of Article objects will be like this:

      Ienumerable<Article> articles = Scraper.Get("http://testing123.com")
          .GetElements("article")
          .MapToObject<Article>(
              (new Mapper())
                .MapText("a", "Title")
                .MapAttr("a", "href", "Url")
                .MapText("div .article-content", "Content"),
                .MapText("div .article-time", "CreationTime")
          );
