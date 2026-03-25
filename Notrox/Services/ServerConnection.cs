using Notrox.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

using Notrox.Model;

namespace Notrox.Services
{
    public class ServerConnection
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private string? Token;

        public ServerConnection(string baseUrl)
        {
            if (!baseUrl.StartsWith("https://")) throw new Exception("Insecure connection not allowed");

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                var jsonData = new
                {
                    Username = username,
                    Password = password
                };

                string jsonString = JsonSerializer.Serialize(jsonData);
                StringContent content = new(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("/UserLogin", content);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                Message message = JsonSerializer.Deserialize<Message>(json, options);

                if (message == null || string.IsNullOrEmpty(message.token))
                {
                    Token = null;
                    _httpClient.DefaultRequestHeaders.Authorization = null;
                    return false;
                }


                Token = message.token;

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Token = null;
                _httpClient.DefaultRequestHeaders.Authorization = null;
                return false;
            }
        }

        public void Logout()
        {
            Token = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<UsersClass?> GetCurrentUser()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/getUser");

                if (!response.IsSuccessStatusCode)
                    return null;

                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UsersClass>(json, options);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async Task<List<UsersClass>> ListUsers()
        {
            List<UsersClass> DataOfUsers = new();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/getAllUsers");
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                DataOfUsers = JsonSerializer.Deserialize<List<UsersClass>>(json, options) ?? new();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataOfUsers;
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"/deleteUser/{id}");
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<List<ProductsClass>> ListProducts()
        {
            List<ProductsClass> DataOfProducts = new();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/getproduct");
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                DataOfProducts = JsonSerializer.Deserialize<List<ProductsClass>>(json, options) ?? new();
            }
            catch
            {
                return DataOfProducts;
            }

            return DataOfProducts;
        }

        public async Task<bool> AddProduct(string Name, string Description, int Price, int Ammount, int CompanyId, string IMGURL)
        {
            try
            {
                var JsonData = new
                {
                    Name,
                    Description,
                    Price,
                    Ammount,
                    CompanyId,
                    IMGURL
                };

                string JsonString = JsonSerializer.Serialize(JsonData);
                StringContent infoToSend = new(JsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("/postproduct", infoToSend);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> EditProduct(int Id, string Name, string Description, int Price, int Ammount, string IMGURL)
        {
            try
            {
                var JsonData = new
                {
                    Name,
                    Description,
                    Price,
                    Ammount,
                    IMGURL
                };

                string JsonString = JsonSerializer.Serialize(JsonData);
                StringContent infoToSend = new(JsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"/updateproduct/{Id}", infoToSend);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProduct(int Id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"/deleteproduct/{Id}");
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<UserAddressesClass> GetUserAddresses(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/getUserAddresses/{id}");
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UserAddressesClass>(json, options);
            }
            catch
            {
                return new UserAddressesClass();
            }
        }

        public async Task<bool> EditAddress(string City, int Zip, string Address1)
        {
            try
            {
                var jsonData = new
                {
                    City,
                    Zip,
                    Address1
                };

                string jsonString = JsonSerializer.Serialize(jsonData);
                StringContent content = new(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync("/EditAddress", content);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditBillingAddress(string City, int Zip, string Address1)
        {
            try
            {
                var jsonData = new
                {
                    City,
                    Zip,
                    Address1
                };

                string jsonString = JsonSerializer.Serialize(jsonData);
                StringContent content = new(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync("/EditBillingAddress", content);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public async Task<List<OrdersClass>> ListOrders()
        {
            List<OrdersClass> DataOfOrders = new();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/getAllOrders");
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                DataOfOrders = JsonSerializer.Deserialize<List<OrdersClass>>(json, options) ?? new();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataOfOrders;
        }

        public async Task<bool> EditOrder(int Id, string Phase)
        {
            try
            {
                var JsonData = new
                {
                    Phase
                };

                string JsonString = JsonSerializer.Serialize(JsonData);
                StringContent infoToSend = new(JsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"/updateOrderPhase/{Id}", infoToSend);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }
    }
}
