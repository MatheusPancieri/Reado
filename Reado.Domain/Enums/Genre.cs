namespace Reado.Domain.Enums;

[Flags] // Indica que o enum pode ser tratado como um conjunto de flags
public enum Genre
{
    None = 0,                // 0
    Action = 1 << 0,        // 1 
    Drama = 1 << 1,         // 2 
    Comedy = 1 << 2,        // 4 
    Thriller = 1 << 3,      // 8 
    Fantasy = 1 << 4,       // 16 
    SciFi = 1 << 5,         // 32 
    Romance = 1 << 6,       // 64 
    Mystery = 1 << 7,       // 128 
    Biography = 1 << 8,     // 256 
    Documentary = 1 << 9,   // 512 
    Horror = 1 << 10,       // 1024 
    Adventure = 1 << 11,    // 2048 
    Historical = 1 << 12,   // 4096 
    Animation = 1 << 13,    // 8192 
    Family = 1 << 14,       // 16384 
    Western = 1 << 15,      // 32768 
    Musical = 1 << 16,      // 65536 
    Sport = 1 << 17,        // 131072 
    Crime = 1 << 18,        // 262144 
    Superhero = 1 << 19     // 524288 
}