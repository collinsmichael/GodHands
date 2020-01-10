using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GodHands {
    // ********************************************************************
    // Publisher for Publish/Subscribe
    // ********************************************************************
    public static class Publisher {
        private static Dictionary<string, object> dict = new Dictionary<string, object>();
        private static Dictionary<string, object> subs = new Dictionary<string, object>();

        // ********************************************************************
        // register an object with the Publisher
        // ********************************************************************
        public static bool Register(string path, object obj) {
            if (!dict.ContainsKey(path)) {
                dict.Add(path, obj);
                subs.Add(path, new List<ISubscriber>());
            }
            return Logger.Pass("Registered "+path);
        }

        // ********************************************************************
        // remove an object from the Publisher
        // ********************************************************************
        public static bool Unregister(string path) {
            if (dict.ContainsKey(path)) {
                dict.Remove(path);
                subs.Remove(path);
            }
            return Logger.Pass("Unregistered "+path);
        }

        // ********************************************************************
        // assign a subscriber to an object
        // ********************************************************************
        public static bool Subscribe(string path, ISubscriber sub) {
            if (!dict.ContainsKey(path) || !subs.ContainsKey(path)) {
                return Logger.Fail(path+" does not exist");
            }

            List<ISubscriber> list = subs[path] as List<ISubscriber>;
            list.Add(sub);
            sub.Notify(dict[path]);
            return true;
        }

        // ********************************************************************
        // remove a subscriber from an object
        // ********************************************************************
        public static bool Unsubscribe(string path, ISubscriber sub) {
            if (!dict.ContainsKey(path) || !subs.ContainsKey(path)) {
                return Logger.Fail(path+" does not exist");
            }

            List<ISubscriber> list = subs[path] as List<ISubscriber>;
            list.Remove(sub);
            return true;
        }

        // ********************************************************************
        // notify all subscribers of an object state change
        // ********************************************************************
        public static bool Publish(string path, object obj) {
            if (!dict.ContainsKey(path) || !subs.ContainsKey(path)) {
                return Logger.Fail(path+" does not exist");
            }

            List<ISubscriber> list = subs[path] as List<ISubscriber>;
            dict[path] = obj;

            foreach (ISubscriber sub in list) {
                sub.Notify(obj);
            }
            return true;
        }
    }
}
