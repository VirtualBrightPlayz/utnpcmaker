using System.Collections.Generic;
using System.Windows.Forms;

namespace UnturnedNPCMaker
{
    class MessageSettings
    {
        string name_i;
        public int totalPages = 0;
        public List<Button> messages = new List<Button>();
        public List<Button> messages2 = new List<Button>();

        public MessageSettings(string name) {
            name_i = name;
        }

        public void addPage(Button message, Button message2) {
            messages.Add(message);
            messages2.Add(message2);
        }

        public void removePage(Button message, Button message2) {
            messages.Remove(message);
            messages2.Remove(message2);
        }

        public bool changeName(List<MessageSettings> list, string name) {
            if (!name.Trim().Equals("")) {
                foreach (MessageSettings msg in list) {
                    if (name == msg.name_i) {
                        return false;
                    }
                }
                name_i = name;
                return true;
            }
            return false;
        }

        public static MessageSettings findMessageSettings(List<MessageSettings> list, string name)
        {
            foreach (MessageSettings msg in list) {
                if (msg.name_i.Equals(name)) {
                    return msg;
                }
            }
            return null;
        }

        public int findTotalPages() {
            return messages.Count;
        }
    }
}