﻿using ConsoleECommerce.Objects;
using ConsoleECommerce.Operations;
using Constants = ConsoleECommerce.Constants;




// I need this TOKEN! 
// I can get it from EBAY interface
// But I do not want to use this interface
// I want to make my program works batch and does not need any action from outside.

Constants.OAuthUserToken = "v^1.1#i^1#f^0#r^0#p^1#I^3#t^H4sIAAAAAAAAAOVYfWwTZRhvtw4zPkZkhi8V6yExEe/6Xq+99s612jEGhbGNtQwYIfN691532/XuuPctW4loncoMMUZjdCZqMgRjVEBRI0ICiDGI8mXM/MM/FJD4EaNE+AMiEeZdO0Y3CSBr4hL7T3PP+7zP+/v9nud53/cOZMeV37dh4Ybzk5y3lPRlQbbE6aQngPJxZXMrSktmljlAgYOzL3tP1tVd+ksVElKqwTdBZOgagu6ulKohPmcMEWlT43UBKYjXhBREPBb5WGRJHe+lAG+YOtZFXSXc0ZoQIQoBkWHFICMmZJaVA5ZVuxwzrocILhCQEyIjyAmWSXilhDWOUBpGNYQFDYcIL/AyJAiSIBCng7yf4f2AYlm6hXA3QxMpuma5UIAI5+DyublmAdZrQxUQgia2ghDhaKQ21hCJ1syvj1d5CmKFB3WIYQGn0fCneboE3c2CmobXXgblvPlYWhQhQoQnnF9heFA+chnMTcDPSQ052gtYn1fwSl6R4aSiSFmrmykBXxuHbVEkUs658lDDCs5cT1FLjUQ7FPHgU70VIlrjtv+WpgVVkRVohoj51ZGVkcZGItwipDoUTUdkNUpGDENVyMamGpIDHC37JCiSflnwMpIfDC6UjzYo84iV5umapNiiIXe9jquhhRqO1MZXoI3l1KA1mBEZ24gK/LxgUEM/F2yxk5rPYhq3aXZeYcoSwp17vH4GhmZjbCqJNIZDEUYO5CQKEYJhKBIxcjBXi4Pl04VCRBvGBu/xdHZ2Up0MpZtJjxcA2rNiSV1MbIMpgbB87V7P+yvXn0AqOSoitGYihccZw8LSZdWqBUBLEmFfwEczl7MwHFZ4pPUfhgLOnuEdUawO4Vg5IUGZhYLfz3IyXYwOCQ8WqcfGARNChkwJZgfEhiqIkBStOkunoKlIPOOXvUxQhqRkLU36OFkmE36JJWkZQgBhIiFywf9To9xoqcegaEJclFovWp1zTLVSG2eZDFq0RkjGl0W78NzOrsyadcvZaF0mbSxduShJL8rQgeTS0I12w1XJz1MVS5m4tX4xBLB7vXgiLNQRhtKo6MVE3YCNuqqImbGVYMaUGgUTZ2JQVS3DqEhafRgtzl5dNHr/cpu4Od7FO6P+o/PpqqyQXbJji5U9H1kBBEOh7BOIEvWUx+51XbCuH7a5NYd6VLwV6+Y6plhbJPNsFSl/5aRydCm0VqRMiPS0ad22qQb7BhbXO6BmnWfY1FUVms30qPs5lUpjIaHCsdbYRShwRRhjhy3NcrSP8flGyUvMHaWtY21LKsZW7Fpwk9dqz/CX/LAj96O7nZ+CbufeEqcTVIE59Gxw97jSZa7SiTORgiGlCDKFlKRmvbuakOqAGUNQzJJKx3nw8yvibwvf2thxqXPNTw+sdxR+Y+hbDaYPfWUoL6UnFHxyAHdcGSmjJ0+b5GVAEATooJ/xgxYw+8qoi57quo28/bT666ZIY3Dt1iniU989vC/+gwEmDTk5nWUOV7fTEeMfbHjoQBvVGjz2+GfrQ9Nm7ZGOH+AeO+radPLjLeUHJ7aeuHDvo28a23dWhE/3HfB/YeyY8+ytJ/aP33S29sj9P545+Q79NsuxKy4sd+w91z9Za/qw/cnxR9LBJ8wZ1b29zxw93rc5++fGhtMvJfoP7XLxL0we+HxV/9MDFS8np6xoS0346wL5+obtlYvDO8dvd2xbfPFU0++Z9+76Zsa7QTaVnbVjzyOffHXWvOjuWfCau6f66++3uGZ2U+emLztydmr/iwe7nzt/6I9th7vaud5XV9f3PB+7M3nmjeaPBk5tXdnzwf4v922+i4ld2r9Ko6m6df7DByGOZCver60/truqcqDyVHugd/e3U3blc/k3FgOkgv0RAAA=";

OrderOperations objOrderOperations = new OrderOperations();

GetOrdersResponse objGetOrdersResponse = objOrderOperations.GetOrders("2023-06-10T08:30:00.000");
foreach (Order o in objGetOrdersResponse.orders)
{
    Console.WriteLine("Order Id : " + o.orderId + " - Creation Date : " + o.creationDate.ToString("dd.MM.yyyy HH:mm:ss"));
}
Console.Read();