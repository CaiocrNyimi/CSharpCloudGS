namespace Skill4Green.API.Hateoas;

public class ResourceWrapper<T>
{
    public T Data { get; set; }
    public List<HateoasLink> Links { get; set; }

    public ResourceWrapper(T data)
    {
        Data = data;
        Links = new List<HateoasLink>();
    }
}