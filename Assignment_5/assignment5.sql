Create database TicketBookingSystem

CREATE TABLE Venue (
    venue_id INT PRIMARY KEY,
    venue_name VARCHAR(100) NOT NULL,
    address NVARCHAR(255) NOT NULL
);

CREATE TABLE Event (
    event_id INT PRIMARY KEY,
    event_name VARCHAR(100) NOT NULL,
    event_date DATE NOT NULL,
    event_time TIME NOT NULL,
    venue_id INT NOT NULL,
    total_seats INT NOT NULL,
    available_seats INT NOT NULL,
    ticket_price DECIMAL(10, 2) NOT NULL,
    event_type NVARCHAR(50) CHECK (event_type IN ('Movie', 'Sports', 'Concert')),
    FOREIGN KEY (venue_id) REFERENCES Venue(venue_id)
);

CREATE TABLE Customer (
    customer_id INT PRIMARY KEY,
    customer_name NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) NOT NULL,
    phone_number NVARCHAR(20) NOT NULL
);

CREATE TABLE Booking (
    booking_id INT PRIMARY KEY,
    customer_id INT NOT NULL,
    event_id INT NOT NULL,
    num_tickets INT NOT NULL,
    total_cost DECIMAL(10, 2) NOT NULL,
    booking_date DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (customer_id) REFERENCES Customer(customer_id),
    FOREIGN KEY (event_id) REFERENCES Event(event_id)
);

INSERT INTO Venue (venue_id,venue_name, address) VALUES
(1,'Royal Theatre', '123 Luffy Street'),
(2,'Sports Arena', '456 ZORO street'),
(3,'Music Hall', '789 Sanji Road');

INSERT INTO Event (event_id,event_name, event_date, event_time, venue_id, total_seats, available_seats, ticket_price, event_type) VALUES
(11,'One piece RED', '2025-05-15', '18:00:00', 1, 200, 200, 1500, 'Movie'),
(12,'India vs Pakistan', '2025-06-20', '20:00:00', 2, 50000, 50000, 2500, 'Sports'),
(13,'Led-Zeppline Chennai tour', '2025-07-10', '19:30:00', 3, 1000, 1000, 2000, 'Concert');

INSERT INTO Customer (customer_id,customer_name, email, phone_number) VALUES
(21,'Vikash', 'vikash@gmai.com', '9876543000'),
(22,'Vignesh', 'vignesh@gmai.com', '9876543001'),
(23,'Venkat', 'venkat@gmai.com', '9876543002'),
(24,'Sunil', 'sunilh@gmai.com', '9876543003'),
(25,'Raj', 'raj@gmai.com', '9876543004'),
(26,'Sri', 'sri@gmai.com', '9876543005'),
(27,'Sudhan', 'sudhan@gmai.com', '9876543006'),
(28,'Sai', 'sai@gmai.com', '9876543007'),
(29,'Subash', 'subash@gmai.com', '9876543008'),
(30,'Dhanush', 'dhanush@gmai.com', '9876543009');

INSERT INTO Booking ( booking_id,customer_id, event_id, num_tickets, total_cost) VALUES
(41,21, 11, 2, 3000),
(42,22, 12, 5, 12500),
(43,23, 13, 3, 6000),
(44,24, 13, 4, 3200),
(45,25, 11, 1, 1500),
(46,26, 12, 6, 15000),
(47,27, 13, 2, 4000),
(48,28, 13, 2, 1600),
(49,29, 11, 3, 4500),
(50,30, 13, 1, 2000);


--Task 2
select * from Event;--1
select * from Event where available_seats > 0;--2
select * from Event where event_name like '%vs%';--3
select * from event where ticket_price between 1000 and 2500;--4
select * from event where event_date between '2025-05-01' and '2025-08-01';--5
select * from event where available_seats > 0 and event_type = 'concert';--6
select * from customer order by customer_id offset 5 rows fetch next 5 rows only;--7
select * from booking where num_tickets > 4;--8
select * from customer where phone_number like '%000';--9
select * from event where total_seats > 15000 order by total_seats;--10
select * from event where event_name not like 'x%' and event_name not like 'y%' and event_name not like 'z%';--11

--TASK 3
select event_name, avg(ticket_price) as average_price from event group by event_name;--1
select e.event_name, sum(b.total_cost) as total_revenue from booking b join event e on b.event_id = e.event_id
group by e.event_name;--2
select top 1 e.event_name, sum(b.num_tickets) as total_tickets from booking b join event e on b.event_id = e.event_id
group by e.event_name order by total_tickets desc;--3
select e.event_name, sum(b.num_tickets) as total_tickets_sold from booking b
join event e on b.event_id = e.event_id group by e.event_name;--4
select event_name from event where event_id not in (select distinct event_id from booking);--5
select top 1 c.customer_name, sum(b.num_tickets) as total_tickets from booking b join customer c on b.customer_id = c.customer_id
group by c.customer_name order by total_tickets desc;--6
select format(booking_date, 'yyyy-mm') as month, sum(num_tickets) as tickets_sold
from booking group by format(booking_date, 'yyyy-mm');--7
select v.venue_name, avg(e.ticket_price) as average_price
from event e join venue v on e.venue_id = v.venue_id group by v.venue_name;--8
select event_type, sum(b.num_tickets) as total_tickets
from booking b join event e on b.event_id = e.event_id
group by event_type;--9
select year(booking_date) as year, sum(total_cost) as total_revenue
from booking group by year(booking_date) order by year;--10
select c.customer_name, count(distinct b.event_id) as events_booked
from booking b join customer c on b.customer_id = c.customer_id group by c.customer_name
having count(distinct b.event_id) > 1;--11
select c.customer_name, sum(b.total_cost) as total_revenue
from booking b join customer c on b.customer_id = c.customer_id
group by c.customer_name;--12
select e.event_type, v.venue_name, avg(e.ticket_price) as average_price
from event e join venue v on e.venue_id = v.venue_id
group by e.event_type, v.venue_name;--13
select c.customer_name, sum(b.num_tickets) as total_tickets
from booking b join customer c on b.customer_id = c.customer_id
where booking_date >= dateadd(day, -30, getdate())
group by c.customer_name;--14


--TASK 4
select v.venue_name,(select avg(ticket_price) from event where venue_id = v.venue_id) as average_ticket_price
from venue v;--1
select event_name from event where (total_seats - available_seats) > (total_seats / 2);--2
select event_name,(select sum(num_tickets) from booking where event_id = e.event_id) as total_tickets_sold
from event e;--3
select customer_name from customer c where not exists (select 1 from booking b where b.customer_id = c.customer_id);--4 every user have booked a ticket
select event_name from event where event_id not in (select distinct event_id from booking);--5 every event have atleast 2 tickets sold
select event_type, sum(tickets_sold) as total_tickets
from ( select e.event_type, b.num_tickets as tickets_sold from booking b join event e on b.event_id = e.event_id) as
sub group by event_type;--6
select event_name, ticket_price from event
where ticket_price > (select avg(ticket_price) from event);--7
select customer_name,(select sum(total_cost) from booking where customer_id = c.customer_id) as total_revenue
from customer c;--8
select customer_name from customer c where exists (select 1 
from booking b join event e on b.event_id = e.event_id where b.customer_id = c.customer_id and e.venue_id = 1);--9
select event_type, (select sum(b.num_tickets) from booking b join event e2 on b.event_id = e2.event_id
where e2.event_type = e.event_type) as total_ticket from event e
group by event_type;--10
select distinct c.customer_name, format(b.booking_date, 'yyyy-mm') as booking_month
from booking b join customer c on b.customer_id = c.customer_id;--11
select venue_name, (select avg(ticket_price) from event where venue_id = v.venue_id) as average_price
from venue v;--12




