using Lab2N3.Models;
using Microsoft.AspNetCore.SignalR;

namespace Lab2N3
{
	public class CartHub : Hub
	{
		public async Task UpdateCart(Dictionary<Product, int> cart)
		{
			await Clients.All.SendAsync("ReceiveUpdatedCart", cart);
		}
	}
}
