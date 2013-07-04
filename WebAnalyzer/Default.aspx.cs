using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace WebAnalyzer
{
    using System.IO;
    using System.Text;

    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Standard;

    using WebAnalyzer.Analyzers;

    public partial class _Default : Page
    {

        public List<Analyzer> Analyzers { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Analyzers = new List<Analyzer>();
            Analyzers.Add(new StandardAnalyzer());
            Analyzers.Add(new KeywordAnalyzer());
            Analyzers.Add(new StopAnalyzer());
            Analyzers.Add(new SimpleAnalyzer());
            Analyzers.Add(new WhitespaceAnalyzer());
            Analyzers.Add(new WordDelimiterAnalyzer());
            Analyzers.Add(new StemmingAnalyzer());
        }

        public string Name
        {
            get { return "Term Frequencies"; }
        }

        Dictionary<string, int> termDictionary = new Dictionary<string, int>();

        public string GetView(TokenStream tokenStream, out int numberOfTokens)
        {
            StringBuilder sb = new StringBuilder();

            Token token = tokenStream.Next();

            numberOfTokens = 0;

            while (token != null)
            {
                numberOfTokens++;

                if (termDictionary.Keys.Contains(token.TermText()))
                    termDictionary[token.TermText()] = termDictionary[token.TermText()] + 1;
                else
                    termDictionary.Add(token.TermText(), 1);

                token = tokenStream.Next();
            }

            foreach (var item in termDictionary.OrderBy(x => x.Key))
            {
                sb.Append(item.Key + " [" + item.Value + "]   ");
            }

            termDictionary.Clear();

            return sb.ToString();
        }


        public string GetTokenView(TokenStream tokenStream, out int numberOfTokens)
        {
            StringBuilder sb = new StringBuilder();

            Token token = tokenStream.Next();

            numberOfTokens = 0;

            while (token != null)
            {
                numberOfTokens++;
                sb.Append(token.TermText() + "   Start: " + token.StartOffset().ToString().PadLeft(5) + "  End: " + token.EndOffset().ToString().PadLeft(5) + "\r\n");
                token = tokenStream.Next();
            }

            return sb.ToString();
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
    }
}