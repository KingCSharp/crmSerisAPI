======== Database Tables ===================================================
[dbo].[AddressLookupValue]			-- Appears to just be a list of counties/cities for Canada and USA
[dbo].[AppData]						-- Stores JSON data for various modules
[dbo].[AutomationConfiguration]		-- Unsure, but I'm guessing it has to do with the AutomationService table
[dbo].[AutomationService]			-- Looks like a schedule for running services
[dbo].[Dealer]						-- Looks like this stores references to other databases (for various types of equipment perhaps?)
[dbo].[Login]						-- Just a list of user logins
[dbo].[LoginHistory]				-- Tracks login dates, emails, and IP addresses
[dbo].[LoginPasswordReset]			-- Stores active login password resets
[dbo].[NylasAccount]				-- Stores emails and account IDs (I assume for something called Nylas)
[dbo].[Pricing]						-- Looks like this stores prices and descriptions. No FK, so there must be another table that utilizes this since there's no product or service attached here
[dbo].[ReportModule]				-- Appears to be JSON stored for some kind of custom reporting solution
[dbo].[SupportTicket]				-- Stores support ticket entries (possibly logged automatically due to the inclusion of the exception type, message, and source page that it occurred on
[dbo].[UserRight]					-- User claims for various site functionality
[dbo].[UserRightCategory]			-- The categories for the user claims
[dbo].[WorkflowModule]				-- Looks like a definition for a data structure for "modules"