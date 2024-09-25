using EM.Data.Entities;
using EM.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<EventOffer> EventOffers { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<EventDocument> EventDocuments { get; set; }
        public DbSet<EventTicketCategory> EventTicketCategories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<TaxConfiguration> TaxConfigurations { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

			modelBuilder.Entity<Organizer>().ToTable("organizer");
			modelBuilder.Entity<Performer>().ToTable("performer");
			modelBuilder.Entity<Venue>().ToTable("venue");
			modelBuilder.Entity<Event>().ToTable("event");
			modelBuilder.Entity<State>().ToTable("state");
			modelBuilder.Entity<City>().ToTable("city");
			modelBuilder.Entity<EventDocument>().ToTable("event_document");
			modelBuilder.Entity<EventOffer>().ToTable("event_offer");
			modelBuilder.Entity<EventTicketCategory>().ToTable("event_ticket_category");
            modelBuilder.Entity<Currency>().ToTable("currency");
            modelBuilder.Entity<TaxConfiguration>().ToTable("tax_configuration");
            modelBuilder.Entity<Country>().ToTable("country");

            modelBuilder.Entity<Organizer>()
                    .HasMany(o => o.Events)
                    .WithOne(og => og.Organizer)
                    .HasForeignKey(p => p.OrganizerId);
            modelBuilder.Entity<Performer>()
                    .HasMany(o => o.Events)
                    .WithOne(og => og.Performer)
                    .HasForeignKey(p => p.PerformerId);
            modelBuilder.Entity<Venue>()
                    .HasMany(o => o.Events)
                    .WithOne(og => og.Venue)
                    .HasForeignKey(p => p.VenueId);
            modelBuilder.Entity<Event>()
                    .HasMany(o => o.EventOffers)
                    .WithOne(og => og.Event)
                    .HasForeignKey(p => p.EventId);
            modelBuilder.Entity<Event>()
                    .HasMany(o => o.EventDocuments)
                    .WithOne(og => og.Event)
                    .HasForeignKey(p => p.EventId);
            modelBuilder.Entity<Event>()
                    .HasMany(o => o.EventTicketCategories)
                    .WithOne(og => og.Event)
                    .HasForeignKey(p => p.EventId);
            modelBuilder.Entity<State>()
                    .HasMany(o => o.Cities)
                    .WithOne(og => og.State)
                    .HasForeignKey(p => p.StateId)
                    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<State>()
                    .HasOne(s => s.TaxConfiguration)
                    .WithOne(tc => tc.State)
                    .HasForeignKey<TaxConfiguration>(tc=>tc.StateId);
            modelBuilder.Entity<Country>()
                    .HasMany(o=>o.States)
                    .WithOne(s => s.Countries)
                    .HasForeignKey(tc=>tc.CountryId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<State>().HasData(StateSeed.GetStates());
            modelBuilder.Entity<City>().HasData(CitySeed.GetCities());
            modelBuilder.Entity<Currency>().HasData(CurrencySeed.GetCurrencies());
            modelBuilder.Entity<Country>().HasData(CountrySeed.GetCountries());
        }

    }
}
