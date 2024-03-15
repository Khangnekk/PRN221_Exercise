
using System.Text.Json;

namespace Lab2N3.Logics
{
	public class CartManager
	{
		private ISession _session;
		private Dictionary<int, int> _cart;

		public CartManager(ISession session)
		{
			_session = session;
			if (session.GetString("cart") != null)
			{
				string data = _session.GetString("cart");
				_cart = JsonSerializer.Deserialize<Dictionary<int, int>>(data);
			}
			if (_cart == null)
			{
				_cart = new Dictionary<int, int>();
				_session.SetString("cart", JsonSerializer.Serialize(_cart));
			}

		}

		public void AddToCart(int productId)
		{
			if (_cart.ContainsKey(productId))
			{
				_cart[productId]++;

			}
			else
				_cart.Add(productId, 1);
			_session.SetString("cart", JsonSerializer.Serialize(_cart));
		}

		public void RemoveFromCart(int productId)
		{
			if (_cart.ContainsKey(productId))
			{
				_cart[productId]--;
				if (_cart[productId] <= 0)
				{
					_cart.Remove(productId);
				}
			}
			else
			{
				_cart.Remove(productId);
			}
			_session.SetString("cart", JsonSerializer.Serialize(_cart));
		}

		public void RemoveTemporaryCart()
		{
			_session.Clear();
		}

		public Dictionary<int, int> GetProducts()
		{
			return _cart;
		}
	}
}
