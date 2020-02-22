using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GodHands {
    // ********************************************************************
    // Recieves notification when the bound object is modified
    // ********************************************************************
    public interface ISubscriber {
        bool Insert(object obj);
        bool Notify(object obj);
        bool Remove(object obj);
    }

    // ********************************************************************
    // Publisher for Publish/Subscribe
    // ********************************************************************
    public static class Publisher {
        public static Dictionary<string, BaseClass> dict = new Dictionary<string, BaseClass>();
        public static Dictionary<string, List<ISubscriber>> subs = new Dictionary<string, List<ISubscriber>>();

        // ********************************************************************
        // generate a unique key
        // ********************************************************************
        private static UInt64 seed = 0;
        public static string KeyGen() {
            UInt64 mul = 0xD5B3E39A85A41697;
            UInt64 mod = 0xFEDE385A4AB19AC3;
            UInt64 key = (++seed*mul) % mod;
            return key.ToString("X16");
        }

        // ********************************************************************
        // register an object with the Publisher
        // ********************************************************************
        public static bool Register(BaseClass obj) {
            if (obj.Key == null) {
                obj.Key = KeyGen();
            }
            if (!dict.ContainsKey(obj.Key)) {
                dict.Add(obj.Key, obj);
            }
            Publish(obj, INSERT);
            return true;
        }

        // ********************************************************************
        // remove an object from the Publisher
        // ********************************************************************
        public static bool Unregister(string key) {
            if (dict.ContainsKey(key)) {
                BaseClass obj = dict[key];
                Publish(obj, REMOVE);
                dict.Remove(key);
            }
            return true;
        }

        // ********************************************************************
        // assign a subscriber to an object
        // ********************************************************************
        public static bool Subscribe(string path, ISubscriber sub) {
            if (!subs.ContainsKey("*")) {
                subs.Add("*", new List<ISubscriber>());
            }
            if (subs.ContainsKey(path)) {
                List<ISubscriber> list = subs[path] as List<ISubscriber>;
                if (!list.Contains(sub)) {
                    list.Add(sub);
                }
            }
            return true;
        }
        public static bool Subscribe(BaseClass obj, ISubscriber sub) {
            if ((obj != null) && (sub != null)) {
                Subscribe(obj.Key, sub);
            }
            return true;
        }

        // ********************************************************************
        // remove a subscriber from an object
        // ********************************************************************
        public static bool Unsubscribe(string path, ISubscriber sub) {
            if (subs.ContainsKey(path)) {
                List<ISubscriber> list = subs[path] as List<ISubscriber>;
                if (list.Contains(sub)) {
                    list.Remove(sub);
                }
            }
            return true;
        }
        public static bool Unsubscribe(BaseClass obj, ISubscriber sub) {
            if ((obj != null) && (sub != null)) {
                Unsubscribe(obj.GetUrl(), sub);
            }
            return true;
        }

        // ********************************************************************
        // notify all subscribers of an object state change
        // ********************************************************************
        private const int INSERT = 1;
        private const int NOTIFY = 2;
        private const int REMOVE = 3;
        public static bool Publish(BaseClass obj, int method=NOTIFY) {
            if (!subs.ContainsKey(obj.Key)) {
                subs.Add(obj.Key, new List<ISubscriber>());
            } else {
                foreach (ISubscriber sub in subs[obj.Key].ToArray()) {
                    if (method == INSERT) sub.Insert(obj);
                    if (method == NOTIFY) sub.Notify(obj);
                    if (method == REMOVE) sub.Remove(obj);
                }
            }

            if (!subs.ContainsKey("*")) {
                subs.Add("*", new List<ISubscriber>());
            } else {
                foreach (ISubscriber sub in subs["*"].ToArray()) {
                    if (method == INSERT) sub.Insert(obj);
                    if (method == NOTIFY) sub.Notify(obj);
                    if (method == REMOVE) sub.Remove(obj);
                }
            }
            return true;
        }
    }
}
