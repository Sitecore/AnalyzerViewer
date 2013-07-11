using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace WebAnalyzer
{
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.CJK;
    using Lucene.Net.Analysis.De;
    using Lucene.Net.Analysis.Ext;
    using Lucene.Net.Analysis.Miscellaneous;
    using Lucene.Net.Analysis.Query;
    using Lucene.Net.Analysis.Shingle;
    using Lucene.Net.Analysis.Snowball;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Analysis.Tokenattributes;
    using Lucene.Net.Util;

    using WebAnalyzer.Analyzers;

    //  using WebAnalyzer.Analyzers;

    //using WebAnalyzer.Analyzers;

    public partial class _Default : Page
    {
        public List<Analyzer> Analyzers { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Analyzers = new List<Analyzer>();
            Analyzers.Add(new StandardAnalyzer(Version.LUCENE_30));
            Analyzers.Add(new KeywordAnalyzer());
            Analyzers.Add(new StopAnalyzer(Version.LUCENE_30));
            Analyzers.Add(new SimpleAnalyzer());
            Analyzers.Add(new WhitespaceAnalyzer());
            Analyzers.Add(new ShingleAnalyzerWrapper(Version.LUCENE_30));
            Analyzers.Add(new SingleCharTokenAnalyzer());
            Analyzers.Add(new UnaccentedWordAnalyzer());
            Analyzers.Add(new StemmingAnalyzer());
        }

        #region Hide Me

        public string Name
        {
            get { return "Term Frequencies"; }
        }

        Dictionary<string, int> termDictionary = new Dictionary<string, int>();

        public string GetView(TokenStream tokenStream, out int numberOfTokens)
        {
            var sb = new StringBuilder();
            var termDictionary = new Dictionary<string, int>();

            var termAttr = tokenStream.GetAttribute<ITermAttribute>();

            while (tokenStream.IncrementToken())
            {
                if (termDictionary.Keys.Contains(termAttr.Term))
                    termDictionary[termAttr.Term] = termDictionary[termAttr.Term] + 1;
                else
                    termDictionary.Add(termAttr.Term, 1);
            }

            foreach (var item in termDictionary.OrderBy(x => x.Key))
            {
                sb.Append(item.Key + " [" + item.Value + "]   ");
            }

            numberOfTokens = termDictionary.Count;
            return sb.ToString();
        }

        public string GetTokenView(TokenStream tokenStream, out int numberOfTokens)
        {


            var sb = new StringBuilder();
            numberOfTokens = 0;

            var termAttr = tokenStream.GetAttribute<ITermAttribute>();
            var startOffset = tokenStream.GetAttribute<Lucene.Net.Analysis.Tokenattributes.IOffsetAttribute>();
            while (tokenStream.IncrementToken())
            {

                sb.Append(termAttr.Term + "   Start: " + startOffset.StartOffset.ToString().PadLeft(5) + "  End: " + startOffset.EndOffset.ToString().PadLeft(5) + "\r\n");

                //var view = "[" + termAttr.Term + "]   ";
                //sb.Append(view);
                numberOfTokens++;
            }

            return sb.ToString();


            //StringBuilder sb = new StringBuilder();

            //Token token = tokenStream.Next();

            //numberOfTokens = 0;

            //while (token != null)
            //{
            //    numberOfTokens++;
            //    sb.Append(token.TermText() + "   Start: " + token.StartOffset().ToString().PadLeft(5) + "  End: " + token.EndOffset().ToString().PadLeft(5) + "\r\n");
            //    token = tokenStream.Next();
            //}

            //return sb.ToString();
        }

        protected void Go_Click(object sender, EventArgs e)
        {
            Result.Text = string.Empty;

            foreach (var analyzer in Analyzers)
            {
                StringReader stringReader = new StringReader(BaseText.Text);

                TokenStream tokenStream = analyzer.TokenStream("defaultFieldName", stringReader);

                int termCounter = 0;
                int numberOfTokens = 0;

                Result.Text = Result.Text + analyzer.GetType() + ": " + this.GetView(tokenStream, out termCounter).Trim() + "\n\n";
            }


            foreach (var analyzer in Analyzers)
            {
                StringReader stringReader = new StringReader(BaseText.Text);

                TokenStream tokenStream = analyzer.TokenStream("defaultFieldName", stringReader);

                int termCounter = 0;
                int numberOfTokens = 0;

                Result.Text = Result.Text + analyzer.GetType() + ": " + this.GetTokenView(tokenStream, out termCounter).Trim() + "\n\n";
            }
        }

        protected void BaseText_TextChanged(object sender, EventArgs e)
        {

            Result.Text = string.Empty;

            Result.Text = Result.Text + "------------------------ Tokenization ----------------------------------\n";

            foreach (var analyzer in Analyzers)
            {
                StringReader stringReader = new StringReader(BaseText.Text);

                TokenStream tokenStream = analyzer.TokenStream("defaultFieldName", stringReader);

                int termCounter = 0;
                int numberOfTokens = 0;

                Result.Text = Result.Text + analyzer.GetType() + ": " + this.GetView(tokenStream, out termCounter).Trim() + "\n\n";
            }

            Result.Text = Result.Text + "------------------------ Position Info ---------------------------------\n";

            foreach (var analyzer in Analyzers)
            {
                StringReader stringReader = new StringReader(BaseText.Text);

                TokenStream tokenStream = analyzer.TokenStream("defaultFieldName", stringReader);

                int termCounter = 0;
                int numberOfTokens = 0;

                Result.Text = Result.Text + analyzer.GetType() + "\n\n" + this.GetTokenView(tokenStream, out termCounter).Trim() + "\n\n";

            }
        }

        #endregion
    }
}