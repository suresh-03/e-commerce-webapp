using System;

public class Product
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public double Price { get; set; }
    public int Rating { get; set; }
    public int RatingCount { get; set; }
    public bool? IsFavorite { get; set; }
    public bool? IsAddedToCart { get; set; }
    public string? ImageURL { get; set; }
	

	public override string ToString()
	{
		return $"Id: {Id}, Name: {Name}, Description: {Description}, Price: {Price}, ImageURL: {ImageURL}, Rating: {Rating}, RatingCount: {RatingCount}, IsAddedToCart: {IsAddedToCart}, IsFavorite: {IsFavorite}";
    }
}
