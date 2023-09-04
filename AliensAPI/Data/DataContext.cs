using Microsoft.EntityFrameworkCore;
using AliensAPI.Models;

namespace AliensAPI.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<AlienModel> Aliens { get; set; }
		public DbSet<PlanetModel> Planets { get; set; }
		public DbSet<PowerModel> Powers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<AlienModel>().HasMany(aln => aln.Powers)
				.WithMany(pwr => pwr.Aliens)
				.UsingEntity<Dictionary<string, object>>(
					"AliensPowers",
					j => j.HasOne<PowerModel>()
						.WithMany()
						.HasForeignKey("PowerId")
						.OnDelete(DeleteBehavior.Restrict),
					j => j.HasOne<AlienModel>()
						.WithMany()
						.HasForeignKey("AlienId")
						.OnDelete(DeleteBehavior.Restrict)
				 );

			modelBuilder.Entity<AlienModel>().HasOne(aln => aln.NativePlanet)
				.WithMany(plt => plt.CurrentNativePopulation)
				.HasForeignKey(aln => aln.NativePlanetId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<AlienModel>().HasOne(aln => aln.OriginPlanet)
				.WithMany(plt => plt.CurrentImmigrantPopulation)
				.HasForeignKey(aln => aln.OriginPlanetId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<AlienModel>().HasOne(aln => aln.CurrentPlanet)
				.WithMany(plt => plt.CurrentTourists)
				.HasForeignKey(aln => aln.CurrentPlanetId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
