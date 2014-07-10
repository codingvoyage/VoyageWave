taskManager = {};
scene = {};
factory = {};


function spawner(threadHandle)
	while true do
		wait(threadHandle, 1000);
		factory:MakeTim();
	end

end


function timBehavior(threadHandle, behaviorInstance)

	while true do
		move(threadHandle, behaviorInstance, 150, 1, 0);
		wait(threadHandle, 2000);
		move(threadHandle, behaviorInstance, 150, 0, 1);
		wait(threadHandle, 2000);
		move(threadHandle, behaviorInstance, 150, -1, 0);
		move(threadHandle, behaviorInstance, 150, 0, -1);

	end
end

function wait(threadHandle, timeInMilliseconds)
	taskManager:Wait(threadHandle, timeInMilliseconds);
	coroutine.yield();
end

function move(threadHandle, behaviorInstance, distance, directionx, directiony)
	taskManager:Move(threadHandle, behaviorInstance, distance, directionx, directiony)
	coroutine.yield();
end









-- Fields
coroutineTable = { };
DEFAULT_LIMIT = 500;
limit = DEFAULT_LIMIT;
currentIndex = 1;

function initializeBridge()

	for i=1,DEFAULT_LIMIT do
		coroutineTable[i] = true;
	end

end

-- LuaFunction is actually a function.
-- Args is a table with the arguments for the
-- specific function
function startCoroutine(luaFunction)

	co = coroutine.create(luaFunction);

	coroutineID = getIndex();

	coroutineTable[coroutineID] = co;

	args = { };
	args.COROUTINE_HANDLE = coroutineHandle;

	coroutine.resume(co, args);

	return coroutineID;
end

-- Responsible for finding new indices,
-- pruning dead coroutines, and increasing
-- the limit when we need more space.
function getIndex()

	limitCount = 0;

	-- Keep looking, unless we overshoot the limit
	while limitCount <= limit do

		current = coroutineTable[currentIndex];

		-- If empty space found, return the index
		-- If it's not empty, but the coroutine is
		-- dead, then return the index. Else, go on.
		if type(current) == 'boolean' or
		   coroutine.status(current) == 'dead' then
			return currentIndex;
		else
			currentIndex = currentIndex + 1;
			limitCount = limitCount + 1;
		end

		-- If our new currentIndex is greater than
		-- the cap, then we start searching from 1 again
		if currentIndex > limit then
			currentIndex = 1;
		end
	end


	-- If we have overshot the limit, expand the
	-- table and increase the limit.

	newLimits = limit + 1;
	limit = limit + DEFAULT_LIMIT;

	for i=newLimits,limit do
		coroutineTable[i] = true;
	end

	return newLimits;

end

function test()
	print ("boop");
	coroutine.yield();
	print ("hay");
end

function resumeCoroutine(coroutineID)
	coroutine.resume(coroutineTable[coroutineID]);
end
