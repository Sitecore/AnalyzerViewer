using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAnalyzer.Analyzers
{
    using System.Collections;

    using Lucene.Net.Analysis;

    /// <summary>
    /// An analyzer that implements a number of filters. Including porter stemming,
    /// Diacritic stripping, and stop word filtering.
    /// </summary>
    public class StemmingAnalyzer : Analyzer
    {
        /// <summary>
        /// A rather short list of stop words that is fine for basic search use.
        /// </summary>
        private static readonly string[] stopWords = new[]
    {
        "0", "1", "2", "3", "4", "5", "6", "7", "8",
        "9", "000", "$", "£",
        "about", "after", "all", "also", "an", "and",
        "another", "any", "are", "as", "at", "be",
        "because", "been", "before", "being", "between",
        "both", "but", "by", "came", "can", "come",
        "could", "did", "do", "does", "each", "else",
        "for", "from", "get", "got", "has", "had",
        "he", "have", "her", "here", "him", "himself",
        "his", "how","if", "in", "into", "is", "it",
        "its", "just", "like", "make", "many", "me",
        "might", "more", "most", "much", "must", "my",
        "never", "now", "of", "on", "only", "or",
        "other", "our", "out", "over", "re", "said",
        "same", "see", "should", "since", "so", "some",
        "still", "such", "take", "than", "that", "the",
        "their", "them", "then", "there", "these",
        "they", "this", "those", "through", "to", "too",
        "under", "up", "use", "very", "want", "was",
        "way", "we", "well", "were", "what", "when",
        "where", "which", "while", "who", "will",
        "with", "would", "you", "your",
        "a", "b", "c", "d", "e", "f", "g", "h", "i",
        "j", "k", "l", "m", "n", "o", "p", "q", "r",
        "s", "t", "u", "v", "w", "x", "y", "z"
    };

        private Hashtable stopTable;

        /// <summary>
        /// Creates an analyzer with the default stop word list.
        /// </summary>
        public StemmingAnalyzer() : this(stopWords) { }

        /// <summary>
        /// Creates an analyzer with the passed in stop words list.
        /// </summary>
        public StemmingAnalyzer(string[] stopWords)
        {
            stopTable = StopFilter.MakeStopSet(stopWords);
        }

        public override TokenStream TokenStream(string fieldName, System.IO.TextReader reader)
        {
            return new PorterStemFilter(new ISOLatin1AccentFilter(new StopFilter(new LowerCaseTokenizer(reader), stopWords)));
        }
    }
}