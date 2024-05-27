// scripts.js

// Function to fetch tenants from the backend API
function fetchTenants() {
    $.get("/api/tenants", function (data) {
        var tenantList = $("#tenantList");
        tenantList.empty();
        $.each(data, function (index, tenant) {
            tenantList.append("<li>" + tenant.tenantId + " "+ tenant.tenantName + "</li>");
        });
    });
}

// Function to fetch users from the backend API
function fetchUsers() {
    $.get("/api/users", function (data) {
        var userList = $("#userList");
        userList.empty();
        $.each(data, function (index, user) {
            userList.append("<li>" + user.id + " " +user.name + "</li>");
        });
    });
}

// Function to fetch users by tenant from the backend API
function getUsersByTenant() {
    
    var tenantKey = $("#tenantKeyInput").val();
    var url = "/api/users/" + tenantKey; 

    $.get(url, function (data) {
        console.log("Data received:", data); 
        var tenantUserList = $("#tenantUserList"); 
        tenantUserList.empty(); 
        
        // Check if data is an array or a single object
        if (Array.isArray(data)) {
            $.each(data, function (index, user) {
                
                tenantUserList.append("<li>" + user.id + " " + user.name + "</li>");
            });
        } else if (typeof data === 'object') {
            // Handle case where data is a single object
            tenantUserList.append("<li>" + data.id + " " + data.name + "</li>");
        } else {
            console.error("Unexpected data format:", data);
            alert("Unexpected data format received.");
        }
    }).fail(function(xhr, status, error) {
        console.error("Error fetching users:", error);
        alert("Error fetching users: " + error);
    });
}



// Function to create a new tenant using the backend API
function createTenant() {
    var tenantId = $("#newTenantId").val(); 
    var newTenantName = $("#newTenantName").val();
    $.ajax({
        url: "/api/tenants",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({ tenantId: tenantId, tenantName: newTenantName }),
        success: function (data) {
            alert("Tenant created successfully!");
            location.reload({ forceReload: true });
            
        },
        error: function (xhr, status, error) {
            console.error("Error creating tenant:", error);
            alert("Error creating tenant: " + error);
        }
    });

}

// Function to create a new user using the backend API
function createUser() {
    var userId = $("#newUserId").val(); 
    var newUserName = $("#newUserName").val();
    var tenantKey = $("#newUserTenantId").val(); 
    console.log("User ID:", userId);
    console.log("User Name:", newUserName);
    console.log("Tenant Key:", tenantKey);
    
    $.ajax({
        url: "/api/users",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({ Id: userId, Name: newUserName, TenantKey: tenantKey }),
        success: function (data) {
            alert("User created successfully!");
            location.reload({ forceReload: true });
            
        },
        error: function (xhr, status, error) {
            console.error("Error creating user:", error);
            alert("Error creating user: " + error);
        }
    }); 
}

// Function to delete a tenant using the backend API
function deleteTenant() {
    var tenantId = $("#tenantIdToDelete").val();
    if (tenantId) {
        $.ajax({
            url: "/api/tenants/" + tenantId,
            method: "DELETE",
            success: function () {
                alert("Tenant deleted successfully!");
                location.reload({ forceReload: true });
                
            },
            error: function (xhr, status, error) {
                console.error("Error deleting tenant:", error);
                alert("Error deleting tenant: " + error);
            }
        });
    } else {
        alert("Please enter a valid Tenant ID.");
    }
}

// Function to delete a user using the backend API
function deleteUser() {
    var userId = $("#userIdToDelete").val();
    if (userId) {
        $.ajax({
            url: "/api/users/" + userId,
            method: "DELETE",
            success: function () {
                alert("User deleted successfully!");
                location.reload({ forceReload: true });
                
            },
            error: function (xhr, status, error) {
                console.error("Error deleting user:", error);
                alert("Error deleting user: " + error);
            }
        });
    } else {
        alert("Please enter a valid User ID.");
    }
}

// Function to update a tenant using the backend API
function updateTenant() {
    var tenantId = $("#tenantIdToUpdate").val();
    var updatedTenantName = $("#updatedTenantName").val();

    if (tenantId && updatedTenantName) {
        $.ajax({
            url: "/api/tenants/" + tenantId,
            method: "PUT",
            contentType: "application/json",
            data: JSON.stringify({ tenantId: tenantId, tenantName: updatedTenantName }),
            success: function () {
                alert("Tenant updated successfully!");
                
                fetchTenants();
                location.reload({ forceReload: true });
            },
            error: function (xhr, status, error) {
                console.error("Error updating tenant:", error);
                alert("Error updating tenant: " + error);
            }
        });
    } else {
        alert("Please enter a valid Tenant ID and Updated Tenant Name.");
    }
}

// Function to update a user using the backend API
function updateUser() {
    var userId = $("#userIdToUpdate").val();
    var updatedUserName = $("#updatedUserName").val();
    var updatedUserTenantId = $("#updatedUserTenantId").val();

    if (userId && updatedUserName && updatedUserTenantId) {
        $.ajax({
            url: "/api/users/" + userId,
            method: "PUT",
            contentType: "application/json",
            data: JSON.stringify({ Id: userId, Name: updatedUserName, TenantKey: updatedUserTenantId }),
            success: function () {
                alert("User updated successfully!");
                
                fetchUsers();
                location.reload({ forceReload: true });
            },
            error: function (xhr, status, error) {
                console.error("Error updating user:", error);
                alert("Error updating user: " + error);
            }
        });
    } else {
        alert("Please enter valid User ID, Updated User Name, and Updated Tenant ID.");
    }
}

// Fetch the initial list of users and tenants
fetchUsers();
fetchTenants();


fetchUsers();
fetchTenants();


