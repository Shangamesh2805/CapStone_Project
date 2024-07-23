# Health Insurance App

## Overview
Welcome to the Health Insurance App! This application provides a seamless experience for users to manage their health insurance policies. With dedicated interfaces for normal customers and insurance agents, the app offers various functionalities including policy updates, renewals, new policy applications, and claims. The app is developed using HTML, CSS, JavaScript, and .NET C# for the backend, with a SQL database and deployment on Azure. Future plans include integrating a chatbot to provide information on insurance schemes.

## Unique Features
- **Insurance Expiry Updates**: Users receive notifications for insurance policies that are expiring within a month.
- **Discounted Renewals**: Customers can renew their policies at a discounted rate if no claims were made in the previous year.
- **Multiple Insurance Options**: Customers can manage multiple insurance policies, including applying for new ones, stopping existing ones, making claims, and renewing policies.
- **Insurance Agent Access**: Agents can view and manage all insurance schemes of their customers.
- **Future Chatbot Integration**: A chatbot will provide information on various insurance schemes.

## Technologies
- **Frontend**: HTML, CSS, JavaScript
- **Backend**: .NET C#
- **Database**: SQL
- **Deployment**: Azure

## API Endpoints

### Customer Endpoints
1. **Apply New Insurance Scheme**
   - **Description**: Allows the customer to apply for a new insurance policy.
    
2. **Stop Existing Scheme**
   - **Description**: Enables the customer to stop an existing insurance policy.
    
3. **Claim Insurance**
   - **Description**: Allows the customer to claim their insurance.
   
4. **Renew Insurance**
   - **Description**: Enables the customer to renew an existing insurance policy.
  
5. **Pay for Renewal**
   - **Description**: Processes payment for the renewal of an insurance policy.
    
6. **View Renewal History**
   - **Description**: Retrieves the renewal history for the user.
  

### Insurance Agent Endpoints
1. **View All Insurance Schemes of Customers**
   - **Description**: Provides the insurance agent with access to view all insurance policies held by customers.
   
2. **Manage Insurance Schemes**
   - **Description**: Allows the insurance agent to manage and update the status of insurance schemes for customers.
   
3. **Add New Insurance Scheme**
   - **Description**: Enables the insurance agent to add new insurance schemes.
    
4. **Update Existing Scheme**
   - **Description**: Allows the insurance agent to update details of existing insurance schemes.
   

## Business Logic

### Renewal Logic
If a customer does not renew their insurance scheme within the designated time frame:
1. **Notification**: The customer will receive reminders starting one month before the expiry date.
2. **Grace Period**: If the customer fails to renew before the expiry date, a grace period of 10 days will be granted.
3. **Lapse of Policy**: If the policy is not renewed within the grace period, it will lapse, and the customer will need to apply for a new policy.

### Discount Logic
If no claims were made in the previous year:
- The customer is eligible for a renewal discount.

### Payment for Renewal
- Customers must pay the renewal amount to complete the renewal process.

### Agent Logic
Insurance agents have the following capabilities:
1. **View Schemes**: Access to view all customer insurance schemes.
2. **Manage Schemes**: Ability to manage and update the status of insurance schemes.
3. **Add New Schemes**: Can add new insurance schemes to the system.
4. **Update Existing Schemes**: Can update details of existing insurance schemes.

## Setup and Deployment

### Frontend
- Developed using HTML, CSS, and JavaScript.

### Backend
- Built with .NET C# Web API.

### Database
- Uses SQL for storing and managing data.

### Deployment
- Deployed on Azure for robust and scalable performance.

## Future Plans
- **Chatbot Integration**: A chatbot will be integrated to provide users with information on various insurance schemes, enhancing user engagement and support.

## Conclusion
The Health Insurance App aims to simplify the management of health insurance policies for both customers and insurance agents. With user-friendly features, robust backend support, and future enhancements like chatbot integration, the app ensures a comprehensive and efficient insurance management experience.
