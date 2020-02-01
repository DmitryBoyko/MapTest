using System.Collections.Generic;

namespace MapTest.Models
{

    public class MainPageModel
    {
        /// <summary>
        /// Фильтр года
        /// </summary>
        public List<int> Years { get; set; } = new List<int>();

        /// <summary>
        /// Фильтр статусов федеральных проектов
        /// </summary>
        public List<KeyValuePair<int, string>> MainFilter { get; set; } = new List<KeyValuePair<int, string>>();
    }
}
