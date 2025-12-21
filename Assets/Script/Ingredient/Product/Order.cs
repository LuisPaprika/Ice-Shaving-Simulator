public class Order
{
    public ProductSO product;
    public int amount;

    public Order(ProductSO product, int amount)
    {
        this.product = product;
        this.amount = amount;
    }
}