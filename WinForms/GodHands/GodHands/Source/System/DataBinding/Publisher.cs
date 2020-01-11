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
            return true;
        }
        public static bool Register(IBound obj) {
            if (obj != null) {
                Register(obj.GetUrl(), obj);
            }
            return true;
        }

        // ********************************************************************
        // remove an object from the Publisher
        // ********************************************************************
        public static bool Unregister(string path) {
            if (subs.ContainsKey(path)) {
                List<ISubscriber> list = subs[path] as List<ISubscriber>;
                if (list != null) {
                    foreach (ISubscriber sub in list.ToArray()) {
                        sub.Notify(null);
                    }
                }
                subs.Remove(path);
            }
            if (dict.ContainsKey(path)) {
                dict.Remove(path);
            }
            return true;
        }
        public static bool Unregister(IBound obj) {
            if (obj != null) {
                Unregister(obj.GetUrl());
            }
            return true;
        }

        // ********************************************************************
        // assign a subscriber to an object
        // ********************************************************************
        public static bool Subscribe(string path, ISubscriber sub) {
            if (!dict.ContainsKey(path) || !subs.ContainsKey(path)) {
                return false; //Logger.Fail(path+" does not exist");
            }

            List<ISubscriber> list = subs[path] as List<ISubscriber>;
            if (!list.Contains(sub)) {
                list.Add(sub);
                //sub.Notify(dict[path]);
            }
            return true;
        }
        public static bool Subscribe(IBound obj, ISubscriber sub) {
            if ((obj != null) && (sub != null)) {
                Subscribe(obj.GetUrl(), sub);
            }
            return true;
        }

        // ********************************************************************
        // remove a subscriber from an object
        // ********************************************************************
        public static bool Unsubscribe(string path, ISubscriber sub) {
            if (!dict.ContainsKey(path) || !subs.ContainsKey(path)) {
                return false; //Logger.Fail(path+" does not exist");
            }

            List<ISubscriber> list = subs[path] as List<ISubscriber>;
            if (list.Contains(sub)) {
                list.Remove(sub);
            }
            return true;
        }
        public static bool Unsubscribe(IBound obj, ISubscriber sub) {
            if ((obj != null) && (sub != null)) {
                Unsubscribe(obj.GetUrl(), sub);
            }
            return true;
        }

        // ********************************************************************
        // notify all subscribers of an object state change
        // ********************************************************************
        public static bool Publish(string path, object obj) {
            if (!dict.ContainsKey(path) || !subs.ContainsKey(path)) {
                return false; //Logger.Fail(path+" does not exist");
            }

            // copy list to array (in case someone subscribes/unsubscribes)
            List<ISubscriber> list = subs[path] as List<ISubscriber>;
            ISubscriber[] array = list.ToArray();
            dict[path] = obj;
            foreach (ISubscriber sub in array) {
                sub.Notify(obj);
            }
            return true;
        }
        public static bool Publish(IBound obj) {
            if (obj != null) {
                Publish(obj.GetUrl(), obj);
            }
            return true;
        }
    }
}
