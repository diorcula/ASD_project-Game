grammar PlayerCommands;

//LEXER

SPACE: ' '+;

MOVE: 'move';
WALK: 'walk';
GO: 'go';
ATTACK: 'attack';
SLASH: 'slash';
STRIKE: 'strike';
PICKUP: 'pickup';
GET: 'get';
DROP: 'drop';
EXIT: 'exit';
LEAVE: 'leave';
SAY: 'say';
SHOUT: 'shout';
REPLACE: 'replace';
PAUSE : 'pause';
RESUME : 'resume';
CREATE_SESSION : 'create_session';
JOIN_SESSION : 'join_session';
REQUEST_SESSIONS : 'request_sessions';
START_SESSION : 'start_session';
INSPECT : 'inspect';
SEARCH : 'search';


FORWARD: 'forward';
UP: 'up';
NORTH: 'north';
BACKWARD: 'backward';
DOWN: 'down';
SOUTH: 'south';
LEFT: 'left';
WEST: 'west';
RIGHT: 'right';
EAST: 'east';
ARMOR: 'armor';
HELMET: 'helmet';
WEAPON: 'weapon';
SLOT: 'slot';

NUMBER: '0' | [0-9]+;
MESSAGE: '"' ~'"'+ '"';
//MESSAGE: ~[\r\n]+;

//PARSER

input: command EOF;

step: NUMBER;
slotdigit: NUMBER;
message: MESSAGE;

command:
    (MOVE | WALK | GO) SPACE direction (SPACE step)? #move |
    (ATTACK | SLASH | STRIKE) SPACE direction #attack |
    (PICKUP | GET) #pickup |
    DROP #drop |
    (EXIT | LEAVE) #exit |
    SAY SPACE message #say |
    SHOUT SPACE message #shout |
    REPLACE #replace |
    PAUSE #pause |
    RESUME #resume |
    CREATE_SESSION SPACE message #createSession |
    JOIN_SESSION SPACE message #joinSession |
    REQUEST_SESSIONS #requestSessions |
    START_SESSION #startSession |
    INSPECT SPACE inventorySlot #inspect |
    SEARCH #search;


forward: FORWARD | UP | NORTH;
backward: BACKWARD | DOWN | SOUTH;
left: LEFT | WEST;
right: RIGHT  | EAST;
direction: forward | backward | left | right;
inventorySlot: ARMOR | HELMET | WEAPON | SLOT SPACE slotdigit;
