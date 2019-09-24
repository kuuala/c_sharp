using System.Collections.Generic;
using System.Linq;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private Database db;
        private Parser parser;

        public Indexer()
        {
            db = new Database();
            parser = new Parser(db, new HashSet<char> { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' });
        }

        public void Add(int id, string documentText)
        {
            parser.ParseNewDoc(id, documentText);
        }

        public List<int> GetIds(string word)
            => db.GetIds(word);

        public List<int> GetPositions(int id, string word)
            => db.GetPositions(id, word);

        public void Remove(int id)
        {
            db.RemoveDocument(id);
        }
    }

    class Database
    {
        private Dictionary<string, Dictionary<int, List<int>>> db
            = new Dictionary<string, Dictionary<int, List<int>>>();

        public void Add(string word, int id, int position)
        {
            if (!db.ContainsKey(word))
                db.Add(word, new Dictionary<int, List<int>>());
            if (!db[word].ContainsKey(id))
                db[word].Add(id, new List<int>());
            db[word][id].Add(position);
        }

        public List<int> GetIds(string word) =>
            db.ContainsKey(word) ? db[word].Keys.ToList() : new List<int>();

        public List<int> GetPositions(int id, string word) =>
            db.ContainsKey(word) && db[word].ContainsKey(id) ? db[word][id] : new List<int>();

        public void RemoveDocument(int id)
        {
            foreach (var doc in db.Values)
                doc.Remove(id);
        }
    }

    class Parser
    {
        private HashSet<char> delimiters;
        private Database db;
        
        public Parser(Database database, HashSet<char> delimiters)
        {
            db = database;
            this.delimiters = delimiters;
        }

        public void ParseNewDoc(int id, string text)
        {
            var startWord = 0;
            while(startWord < text.Length)
            {
                while (startWord < text.Length && delimiters.Contains(text[startWord]))
                    ++startWord;
                if (startWord == text.Length)
                    break;
                var currentInd = startWord;
                while (currentInd < text.Length && !delimiters.Contains(text[currentInd]))
                    ++currentInd;
                var length = currentInd - startWord;
                var word = text.Substring(startWord, length);
                db.Add(word, id, startWord);
                startWord += length;
            }
        }
    }
}
