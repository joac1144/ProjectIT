using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Client.Components.Filter;

public partial class FilterPanelTopics
{
    private class CategoryStatus
    {
        public TopicCategory Category { get; set; }

        public bool Collapsed { get; set; } = true;
    }

    [Parameter]
    public EventCallback<FilterTagTopic> DataChanged { get; init; }

    [Parameter]
    public EventCallback<IList<FilterTagTopic>> OnInitializedData { get; set; }
    
    public IList<FilterTagTopic> Data { get; set; } = new List<FilterTagTopic>();

    public IList<FilterTagTopic> ShownData { get; set; } = null!;

    private readonly IList<CategoryStatus> _categories = new List<CategoryStatus>();

    protected override void OnInitialized()
    {
        // Make API call to get all existing topics in database.
        // Temporary solution:
        var topics = new Topic[]
        {
            new Topic
            {
                Name = "Machine learning",
                Category = TopicCategory.ArtificialIntelligence
            },
            new Topic
            {
                Name = "Eye-tracking",
                Category = TopicCategory.ArtificialIntelligence
            },
            new Topic
            {
                Name = "Cryptography",
                Category = TopicCategory.Security
            },
            new Topic
            {
                Name = "Penetration testing",
                Category = TopicCategory.Security
            },
            new Topic
            {
                Name = "Software engineering",
                Category = TopicCategory.SoftwareEngineering
            }
        };

        foreach (Topic topic in topics)
        {
            Data.Add(new FilterTagTopic { Tag = topic.Name, Category = topic.Category });
        }

        foreach (TopicCategory topicCategory in Enum.GetValues<TopicCategory>())
        {
            _categories.Add(new CategoryStatus { Category = topicCategory });
        }

        ShownData = Data;

        if (OnInitializedData.HasDelegate)
        {
            OnInitializedData.InvokeAsync(Data.Select(ft => new FilterTagTopic { Tag = ft.Tag, Category = ft.Category }).ToList());
        }
    }

    private Task CheckboxChecked(ChangeEventArgs e, FilterTagTopic filterTag)
    {
        Data.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = (bool)e.Value!;

        return DataChanged.InvokeAsync(filterTag);
    }

    private void SearchTopics(string value)
    {
        // Show all categories and topics if search field is empty.
        if (string.IsNullOrEmpty(value))
        {
            foreach (CategoryStatus category in _categories)
            {
                category.Collapsed = true;
            }
            ShownData = Data;
        }

        // Filter topics based on search but only if search string length is greater than 2 to prevent too big of a search.
        if (value.Length >= 2)
        {
            string valueLowerCase = value.ToLower();
            ShownData = Data.Where(ft => ft.Tag.ToLower().StartsWith(valueLowerCase) || ft.Tag.ToLower().Contains(valueLowerCase)).ToList();
            foreach (CategoryStatus category in _categories)
            {
                category.Collapsed = false;
            }
        }
    }
}