# Configuration and Local Setup on Windows

## RabbitMQ Setup Guide for Windows

Follow these steps to install and configure RabbitMQ locally.

__Prerequisites__
Ensure you have Erlang OTP installed on your system. RabbitMQ requires Erlang to run.
Installation Steps
__Step 1: Install Erlang OTP__
Download the Erlang OTP installer from the Erlang official website.
Run the downloaded installer and follow the on-screen instructions to complete the installation.
After installation, verify Erlang is correctly installed by opening a command prompt and typing erl. You should enter the Erlang shell.
__Step 2: Install RabbitMQ__
Download the RabbitMQ server installer from the RabbitMQ official website.
Run the downloaded installer. Follow the instructions, and complete the installation process.
RabbitMQ will be installed as a Windows service and start automatically.
__Step 3: Enable RabbitMQ Management Plugin__
The RabbitMQ Management Plugin provides a user-friendly web interface for managing your RabbitMQ server.

Open a Command Prompt as Administrator.
Navigate to your RabbitMQ sbin directory. This is typically found at C:\Program Files\RabbitMQ Server\rabbitmq_server-<version>\sbin.
Run the following command to enable the management plugin:
bash
Copy code
rabbitmq-plugins enable rabbitmq_management
The plugin will be enabled, and the management interface will be available.
Step 4: Access RabbitMQ Management UI
Open a web browser and navigate to [localhost](http://localhost:15672/).
The default login credentials are:
Username: guest
Password: guest
After logging in, you will have access to the RabbitMQ Management UI where you can manage exchanges, queues, bindings, and more.
Verifying RabbitMQ Service Status
You can check the status of the RabbitMQ service anytime by using the following command in an Administrator Command Prompt:

rabbitmqctl status

This command provides detailed information about the RabbitMQ server's running status.

## SQL Server Express Setup Guide for Windows
This README provides a step-by-step guide on installing and configuring SQL Server Express on Windows. SQL Server Express is a free, lightweight edition of SQL Server, ideal for development and small production environments.

__Prerequisites__
Windows operating system.
Internet connection for downloading the installer.
Installation Steps
__Step 1: Download SQL Server Express__
Navigate to the SQL Server Downloads: Go to the SQL Server downloads page.
Select the Express Edition: Click on the "Download now" link under the "Express" edition section.
__Step 2: Run the Installer__
Launch the Installer: Once the download completes, run the installer executable.
Choose the Installation Type: The installer provides options for a basic, custom, or download media installation. For most users, the "Basic" installation is sufficient. Click on "Basic".
__Step 3: Accept the License Terms__
Agree to the License Terms: Read the license agreement, then check the box to accept the terms and conditions.
Click "Install": Proceed with the installation.
__Step 4: Configure SQL Server__
After installation, you might need to perform some basic configurations to get started:

Open SQL Server Management Studio (SSMS): SSMS is a separate download. If you haven't installed it yet, download SSMS and install it.
Connect to the Server: Launch SSMS, and connect to the local SQL Server Express instance. The server name for a default instance is .\SQLEXPRESS.
Create a Database: Once connected, you can create a new database by right-clicking on the "Databases" folder and selecting "New Database".
__Step 5: Enable TCP/IP (Optional)__
By default, SQL Server Express is configured to allow connections only from the local machine. To enable remote connections:

Open SQL Server Configuration Manager: Search for it in the Start menu.
Enable TCP/IP:
Navigate to "SQL Server Network Configuration" > "Protocols for SQLEXPRESS".
Right-click on "TCP/IP" and select "Enable".
Restart SQL Server: In the "SQL Server Services" section, right-click on "SQL Server (SQLEXPRESS)" and choose "Restart".
Step 6: Configure Firewall (Optional)
If you want to allow remote connections to your SQL Server Express instance, you may need to configure the Windows Firewall:

Open Windows Firewall: Go to Control Panel > System and Security > Windows Defender Firewall > Advanced Settings.
Add an Inbound Rule:
Click "New Rule" > "Port" > "TCP", and specify the port used by SQL Server (default is 1433).
Follow the prompts to name and enable the rule.
Conclusion
You now have SQL Server Express installed and configured on your Windows machine. You can begin developing your applications, managing databases, or learning SQL Server functionalities with this lightweight, yet powerful database system.

Remember, SQL Server Express has limitations compared to the full version of SQL Server, such as database size restrictions and reduced performance features. However, it remains an excellent option for small applications, lightweight web sites, and learning purposes.

## Application Setup Guide for Windows

__You have the option to configure either an in-memory store or SQL Server Express as the database.__

![image](https://github.com/aesteves900/requestService/assets/5515535/99090418-9385-4d0a-8102-b8fd40ad5fe0)



__Step 1: Generate a JWT token through the Authentication API.__

![image](https://github.com/aesteves900/requestService/assets/5515535/154a9069-7dae-4b5b-b016-22971326fb64)

__Step2: For instance, you can input the Bearer token eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MTAyMDE5MTl9.8rPC3Y_tPPxgwCcD4QSsCDB0LSVZdJ6-pTcTT7TUUbY into Swagger by clicking on the "Authorize" button.__

![image](https://github.com/aesteves900/requestService/assets/5515535/dc7154d1-307a-4aff-9526-4eb33f2250ea)


__Step 3: For all requests the process is the same__

Create a product by using the POST method, as illustrated in the image below:

![image](https://github.com/aesteves900/requestService/assets/5515535/03de27b4-fbbe-47e3-9468-c72c5d6e353f)

The API belows post data into RebitMQ

![image](https://github.com/aesteves900/requestService/assets/5515535/ade48ef5-6b46-41ed-9ff7-b599266217ee)


 [RabitMQ Admin Toll](http://localhost:15672/)

 user: guest
 pass: guest 

 ## RabitMQ Queue

 ![image](https://github.com/aesteves900/requestService/assets/5515535/4efce170-08b2-47c8-8192-0c8bbf125080)

__Published messages:__

![image](https://github.com/aesteves900/requestService/assets/5515535/692327b6-b29d-4a50-96cc-f3a5a612d03b)




















