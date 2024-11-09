using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Apiapp;

namespace Apiapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsController : ControllerBase
    {
        private readonly Listcs _habits;

        public HabitsController(Listcs habits)
        {
            _habits = habits;
        }

        
        [HttpGet]
        public ActionResult GetHabits(bool? isCompleted)
        {
            if (isCompleted.HasValue)
            {
              
                var filteredHabits = _habits.Habits.Where(h => h.ProcentDone == (isCompleted.Value ? 100 : 0)).ToList();
                return Ok(filteredHabits);
            }

            return Ok(_habits.Habits);
        }

      
        [HttpGet("{id}")]
        public ActionResult<Habits> GetHabit(int id)
        {
            var habit = _habits.Habits.FirstOrDefault(x => x.Id == id);

            if (habit == null)
                return NotFound(); 
            return Ok(habit); 
        }

        // POST api/habits
        [HttpPost]
        public ActionResult PostHabit([FromBody] Habits habit)
        {
            if (habit == null || string.IsNullOrWhiteSpace(habit.Name) || habit.ProcentDone < 0 || habit.ProcentDone > 100)
            {
                return BadRequest("Invalid habit data"); 
            }

         
            if (habit.Id == 0)
            {
                habit.Id = _habits.Habits.Max(x => x.Id) + 1; 
                _habits.Habits.Add(habit);
            }
            else
            {
                var existingHabit = _habits.Habits.FirstOrDefault(x => x.Id == habit.Id);
                if (existingHabit == null)
                {
                    return NotFound(); 
                }

                // Обновление существующей задачи
                existingHabit.Name = habit.Name;
                existingHabit.Description = habit.Description;
                existingHabit.ProcentDone = habit.ProcentDone;
            }

            return CreatedAtAction(nameof(GetHabit), new { id = habit.Id }, habit); 
        }

        // PUT api/habits/{id}
        [HttpPut("{id}")]
        public ActionResult PutHabit(int id, [FromBody] Habits habit)
        {
            if (habit == null || string.IsNullOrWhiteSpace(habit.Name) || habit.ProcentDone < 0 || habit.ProcentDone > 100)
            {
                return BadRequest("Invalid habit data");
            }

            var existingHabit = _habits.Habits.FirstOrDefault(x => x.Id == id);
            if (existingHabit == null)
            {
                return NotFound();
            }

            // Обновление задачи
            existingHabit.Name = habit.Name;
            existingHabit.Description = habit.Description;
            existingHabit.ProcentDone = habit.ProcentDone;

            return Ok(existingHabit); 
        }

        // DELETE api/habits/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteHabit(int id)
        {
            var habit = _habits.Habits.FirstOrDefault(x => x.Id == id);
            if (habit == null)
            {
                return NotFound(); 
            }

            _habits.Habits.Remove(habit); 

            return NoContent();
        }
    }
}
